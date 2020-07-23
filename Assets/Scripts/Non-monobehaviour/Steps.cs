using System.Collections;

public class Steps {
    private ArrayList shownWork;
    private float answer;

    //constructor starts with the unchanged equation
    public Steps(string baseEquation) {
        shownWork = new ArrayList {
            baseEquation
        };
    }

    //add a step to the end of the list
    public void AddStep(string newStep) {
        shownWork.Add(newStep);
    }

    // Replaces the last value with the given parameter
    public void ReplaceLastValue(string value) {
        shownWork.Insert(shownWork.Count - 1, value);
    }

    //toString for printing
    override
    public string ToString() {
        string output = (string) shownWork[0];

        for (int i = 1; i < shownWork.Count; i++) {
            output += "\n" + shownWork[i];
        }
        return output;
    }

    //gets the most recent step. At the end of the problem, this will be the answer (Ex: a = 4.2)
    public string GetLastStep() {
        return (string) shownWork[shownWork.Count - 1];
    }

    //gets the answer but as a parsable float
    public float GetNumericalAnswer() { return this.answer; }

    public void SetAnswer(float answer) { this.answer = answer; }
}