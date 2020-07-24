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
        }
        else {
            Labels.SetActive(false);
            SolveButton.SetActive(false);
        }
    }

    public void SolveWithUserInfo() {
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

            Debug.Log(KinematicSolver.SolveEarth(quantities));
        }
    }

    private string[] CreateStringArray(GameObject[] textObjects) {
        string[] result = new string[textObjects.Length];

        for(int i = 0; i < textObjects.Length; i ++) {
            result[i] = textObjects[i].GetComponent<TMP_InputField>().text;
        }

        return result;
    }
}
