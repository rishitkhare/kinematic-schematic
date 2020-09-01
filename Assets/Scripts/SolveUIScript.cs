using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SolveUIScript : MonoBehaviour
{


    public GameObject[] DropdownArray;
    public GameObject SolveButton;
    public GameObject Labels;
    public GameObject ErrorMessage;

    public GameObject AnswerBox;
    public GameObject AnswerText;

    public GameObject BinaryAnswerBox;
    public GameObject AnswerTextBinary1;
    public GameObject AnswerTextBinary2;

    //these store the values of the user input textboxes
    public GameObject[] EarthArray;
    public GameObject[] EarthUnits;

    public GameObject[] WaterArray;
    public GameObject[] WaterUnits;

    public GameObject[] FireArray;
    public GameObject[] FireUnits;

    public GameObject[] AirArray;
    public GameObject[] AirUnits;

    public GameObject[] ShadowArray;
    public GameObject[] ShadowUnits;

    public GameObject[] GeneralArray;
    public GameObject[] GeneralUnits;


    int equationChoice;

    public void SetVariableFieldsActive(int val) {

        //disables any other option results
        foreach(GameObject variableSet in DropdownArray) {
            variableSet.SetActive(false);
        }

        equationChoice = val;

        //activates the correct set of fields based on user choice
        if(val != 0) {
            DropdownArray[val - 1].SetActive(true);
            SolveButton.SetActive(true);
            Labels.SetActive(true);
        }
        else {
            Labels.SetActive(false);
            SolveButton.SetActive(false);
        }
    }

    public void SolveWithUserInfo() {
        Steps finalAnswer = null;
        Steps finalAnswer2 = null;
        bool displayTwoAnswers = false;

        //clear current window
        AnswerBox.SetActive(false);
        BinaryAnswerBox.SetActive(false);

        try {
            string[] quantities = new string[5];

            //reference semantics cost me 6 hours of sleep omg
            //manually copies array to avoid referencing the same object
            for (int i = 0; i < 5; i ++) {
                quantities[i] = KinematicSolver.emptyVariableSet[i];
            }

            if(equationChoice == 1) { //Earth
                string[] earthStrings = CreateStringArray(EarthArray);
                string[] earthUnits = CreateStringArray(EarthUnits);
                int[] order = { 1, 0, 3, 2 };

                for(int i = 0; i < 4; i ++) {
                    if(!String.IsNullOrEmpty(earthStrings[i])) {

                        //add units if specified
                        if(!String.IsNullOrEmpty(earthUnits[i])) {
                            quantities[order[i]] = earthStrings[i] + " " + earthUnits[i];
                        }
                        else {
                            quantities[order[i]] = earthStrings[i];
                        }
                    }
                }

                finalAnswer = KinematicSolver.SolveEarth(quantities);
            }
            if(equationChoice == 2) {
                string[] waterStrings = CreateStringArray(WaterArray);
                string[] waterUnits = CreateStringArray(WaterUnits);
                int[] order = { 4, 0, 1, 2 };

                for (int i = 0; i < 4; i++) {
                    if (!String.IsNullOrEmpty(waterStrings[i])) {

                        //add units if specified
                        if (!String.IsNullOrEmpty(waterUnits[i])) {
                            quantities[order[i]] = waterStrings[i] + " " + waterUnits[i];
                        }
                        else {
                            quantities[order[i]] = waterStrings[i];
                        }
                    }
                }


                //Display answer
                finalAnswer = KinematicSolver.SolveWater(quantities);
            }
            if(equationChoice == 3) {
                string[] fireStrings = CreateStringArray(FireArray);
                string[] fireUnits = CreateStringArray(FireUnits);
                int[] order = { 1, 0, 3, 4 };

                for (int i = 0; i < 4; i++) {
                    if (!String.IsNullOrEmpty(fireStrings[i])) {

                        //add units if specified
                        if (!String.IsNullOrEmpty(fireUnits[i])) {
                            quantities[order[i]] = fireStrings[i] + " " + fireUnits[i];
                        }
                        else {
                            quantities[order[i]] = fireStrings[i];
                        }
                    }
                }


                //Display answer
                finalAnswer = KinematicSolver.SolveFire(quantities);
            }
            if(equationChoice == 4) {
                string[] airStrings = CreateStringArray(AirArray);
                string[] airUnits = CreateStringArray(AirUnits);
                int[] order = { 4, 0, 3, 2 };

                for (int i = 0; i < 4; i++) {
                    if (!String.IsNullOrEmpty(airStrings[i])) {

                        //add units if specified
                        if (!String.IsNullOrEmpty(airUnits[i])) {
                            quantities[order[i]] = airStrings[i] + " " + airUnits[i];
                        }
                        else {
                            quantities[order[i]] = airStrings[i];
                        }
                    }
                }


                //Display answer
                finalAnswer = KinematicSolver.SolveAir(quantities);
            }
            if(equationChoice == 5) {
                string[] shadowStrings = CreateStringArray(ShadowArray);
                string[] shadowUnits = CreateStringArray(ShadowUnits);
                int[] order = { 4, 1, 3, 2 };

                for (int i = 0; i < 4; i++) {
                    if (!String.IsNullOrEmpty(shadowStrings[i])) {

                        //add units if specified
                        if (!String.IsNullOrEmpty(shadowUnits[i])) {
                            quantities[order[i]] = shadowStrings[i] + " " + shadowUnits[i];
                        }
                        else {
                            quantities[order[i]] = shadowStrings[i];
                        }
                    }
                }


                //Display answer
                finalAnswer = KinematicSolver.SolveShadow(quantities);
            }
            if(equationChoice == 6) {
                string[] generalStrings = CreateStringArray(GeneralArray);
                string[] generalUnits = CreateStringArray(GeneralUnits);
                int[] order = { 0, 1, 2, 3, 4 };

                for (int i = 0; i < 5; i++) {
                    if (!String.IsNullOrEmpty(generalStrings[i])) {

                        //add units if specified
                        if (!String.IsNullOrEmpty(generalUnits[i])) {
                            quantities[order[i]] = generalStrings[i] + " " + generalUnits[i];
                        }
                        else {
                            quantities[order[i]] = generalStrings[i];
                        }
                    }
                }

                if (Mathf.Sign(float.Parse(quantities[1])) == Mathf.Sign(float.Parse(quantities[3])) && Mathf.Sign(float.Parse(quantities[3])) == Mathf.Sign(float.Parse(quantities[4]))) {
                    //Shadow special case

                    Steps[] answers = Algebra.ShadowSpecialCase(quantities);

                    displayTwoAnswers = true;
                    finalAnswer = answers[0];
                    finalAnswer2 = answers[1];
                }
                else {

                    //Display answer
                    finalAnswer = KinematicSolver.SolveGeneral(quantities);

                    if (HasAnyUnknowns(quantities)) {
                        displayTwoAnswers = true;
                        finalAnswer2 = KinematicSolver.SolveGeneral(quantities);
                    }
                }

            }
        } catch (System.SystemException a) {
            Debug.Log(a);
            ErrorMessage.SetActive(true);
            ErrorMessage.GetComponent<ErrorMessageVanish>().CreateErrorMessage();
            return;
        }

        if(displayTwoAnswers) {
            BinaryAnswerBox.SetActive(true);

            AnswerTextBinary1.GetComponent<TMP_Text>().text = finalAnswer.ToString();
            AnswerTextBinary2.GetComponent<TMP_Text>().text = finalAnswer2.ToString();
        }
        else {
            AnswerBox.SetActive(true);
            AnswerText.GetComponent<TMP_Text>().text = finalAnswer.ToString();
        }
    }

    private string[] CreateStringArray(GameObject[] textObjects) {
        string[] result = new string[textObjects.Length];

        for(int i = 0; i < textObjects.Length; i ++) {
            result[i] = textObjects[i].GetComponent<TMP_InputField>().text;
        }

        return result;
    }

    private bool HasAnyUnknowns(string[] quantities) {
        for(int i = 0; i < 5; i ++) {
            if (quantities[i].Equals(KinematicSolver.emptyVariableSet[i])) {
                return true;
            }
        }

        return false;
    }
}
