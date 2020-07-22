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

    public static void main(string[] args) {

    }
}
