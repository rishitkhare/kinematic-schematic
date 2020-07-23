using System.Collections;
using System.Collections.Generic;

public class KinematicEquation {
    // Represents whether each quantity is known {Vi, Vf, Δt, a, ΔX}
    private bool[] knownQuantities = new bool[5];
    private string[] quantities = { "Vi", "Vf", "Δt", "a", "ΔX" };

    protected Steps work;

    protected Expression leftSide;
    protected Expression rightSide;

    protected int absentQuantityIndex = -1;

    public KinematicEquation() {}

    //constructor takes the knowns and values
    public KinematicEquation(bool[] knowns, string[] values) {
        this.knownQuantities = knowns;
        this.quantities = values;
    }

    //method for actually solving equation
    public void DoAlgebra() {
        //should never be called from a generic KinematicEquation object
        //(overidden by subclasses)
    }

    //*** ACCESSORS ***\\

    //accessor for work
    public string GetWorkString() { return work.ToString(); }
}
