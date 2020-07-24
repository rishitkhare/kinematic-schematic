using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicEquation {

    // Represents whether each quantity is known {Vi, Vf, Δt, a, ΔX}
    private bool[] knownQuantities = new bool[5];
    private string[] quantities = { "Vi", "Vf", "Δt", "a", "ΔX" };

    protected Steps work;

    protected Expression leftSide;
    protected Expression rightSide;

    protected int absentQuantityIndex = -1;

    public KinematicEquation() { }

    public KinematicEquation(bool[] knowns, string[] values) {
        this.knownQuantities = knowns;
        this.quantities = values;
    }


    //method for actually solving equation
    public virtual void DoAlgebra() {
        //should never be called from a generic KinematicEquation object
        //(overidden by subclasses)
    }


    //*** ACCESSORS ***\\

    //accessor for work
    public string GetWorkString() {
        return work.ToString();
    }

    public Steps GetWork() { return work; }

    //accessor method for all quantities
    public string GetQuantity(int quantityIndex) {
        return quantities[quantityIndex];
    }

    //accesses quantities but as float
    public float GetNumericalQuantity(int quantityIndex) {
        if (Algebra.IsNumber(quantities[quantityIndex])) {
            return float.Parse(quantities[quantityIndex]);
        }
        throw new System.ArgumentException("ERROR: " + quantities[quantityIndex] + " is not a number");
    }

    public Expression GetLeftSide() {
        return this.leftSide;
    }

    public Expression GetRightSide() {
        return this.rightSide;
    }

    //accessor for knownQuantities
    public bool[] GetKnownQuantities() {
        return this.knownQuantities;
    }

    //accessor for quantities
    public string[] GetQuantities() {
        return quantities;
    }

    //counts number of 'true' in knownQuantities
    public int NumberOfKnownQuantities() {
        int count = 0;
        for (int index = 0; index < knownQuantities.Length; index++) {
            if (knownQuantities[index] && index != absentQuantityIndex) {
                count++;
            }
        }
        return count;
    }


    //*** MUTATORS ***\\

    //sets quantity in array and updates knownQuantities
    public void SetQuantity(int quantityIndex, string quantity) {
        string quantityLower = quantity.ToLower(); //equalsIgnoreCase equivalent
        if (!quantityLower.Equals("?")) {
            quantities[quantityIndex] = quantity;
            knownQuantities[quantityIndex] = true;
        }
    }

    //mutator for work
    public void SetWork(Steps work) {
        this.work = work;
    }

    //mutator for leftSide
    public void SetLeftSide(Expression leftSide) {
        this.leftSide = leftSide;
    }

    //mutator for rightSide
    public void SetRightSide(Expression rightSide) {
        this.rightSide = rightSide;
    }

    //mutator for knownQuantities
    public void SetKnownQuantities(bool[] knownQuantities) {
        this.knownQuantities = knownQuantities;
    }

    //mutator for quantities
    public void SetQuantities(string[] quantities) {
        this.quantities = quantities;
    }

    //*** OTHER METHODS ***\\

    //returns the answer by getting the last step of the work (ex: Vi = 5.0)
    public string GetAnswer() {
        return work.GetLastStep();
    }

    //checks for the first unknown on the list
    public int GetMissingQuantityIndex() {
        for (int quantityIndex = 0; quantityIndex < knownQuantities.Length; quantityIndex++) {
            if (knownQuantities[quantityIndex] == false && quantityIndex != absentQuantityIndex) {
                return quantityIndex;
            }
        }
        throw new System.ArgumentException("ERROR: All values are known");
    }

    //Used for special case when solving quadratic equation in AIR
    public bool IsTimeKnown() {
        return knownQuantities[2];
    }

    //
    public void CheckNumberOfQuantities(int quantities) {
        if (NumberOfKnownQuantities() == 4) {
            // if all four quantities of equation are known, then throw error (error may differ based on correctness of values given)
            if (Algebra.IsEqualEquation(leftSide, rightSide)) {
                throw new System.ArgumentException("ERROR: Equation length - All quantities are known");
            }
            else {
                throw new System.ArgumentException("ERROR: Equation length - Unequal equation");
            }
        }
        if (NumberOfKnownQuantities() < 3) {
            // if there isn't enough information, throw error
            throw new System.ArgumentException("ERROR: Not enough information");
        }
    }
}
