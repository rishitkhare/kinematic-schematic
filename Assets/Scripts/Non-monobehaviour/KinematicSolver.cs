using UnityEngine;

public class KinematicSolver {

    // when passing a string[] of quantities to any of the solve methods, the unknowns should match this list
    public static readonly string[] emptyVariableSet = {"Vi", "Vf", "Δt", "a", "ΔX"};

    // prevents instantiation
    private KinematicSolver() { }


    // solves an equation with 3 or more unknowns
    public static Steps SolveGeneral(string[] quantities) {

        ConvertUnitsInArray(quantities);

        bool[] knowns = ConstructKnowns(quantities);
        if (know3SharedValues(knowns, Earth.presentQuantities)) {
            Steps answer = SolveEarth(quantities);
            quantities[GetUnkownSharedIndex(knowns, Earth.presentQuantities)] = answer.GetNumericalAnswer().ToString();
            return answer;
        }
        else if (know3SharedValues(knowns, Water.presentQuantities)) {
            Steps answer = SolveWater(quantities); //full work
            quantities[GetUnkownSharedIndex(knowns, Water.presentQuantities)] = answer.GetNumericalAnswer().ToString(); ;
            return answer;
        }
        else if (know3SharedValues(knowns, Fire.presentQuantities)) {
            Steps answer = SolveFire(quantities);
            quantities[GetUnkownSharedIndex(knowns, Fire.presentQuantities)] = answer.GetNumericalAnswer().ToString(); ;
            return answer;
        }
        else if (know3SharedValues(knowns, Air.presentQuantities)) {
            Steps answer = SolveAir(quantities);
            quantities[GetUnkownSharedIndex(knowns, Air.presentQuantities)] = answer.GetNumericalAnswer().ToString(); ;
            return answer;
        }
        else if (know3SharedValues(knowns, Shadow.presentQuantities)) {
            Steps answer = SolveShadow(quantities);
            quantities[GetUnkownSharedIndex(knowns, Shadow.presentQuantities)] = answer.GetNumericalAnswer().ToString(); ;
            return answer;
        }
        else {
            throw new System.ArgumentException("ERROR: not enough info");
        }  
    }

    // solves an equation using earth with correct unknowns
    public static Steps SolveEarth(string[] quantities) {

        ConvertUnitsInArray(quantities);

        //construct an array that tells us which ones are known
        if (quantities.Length != 5) {
            throw new System.ArgumentException("invalid set of quantites");
        }

        bool[] knowns = ConstructKnowns(quantities);

        Earth solution = new Earth(knowns, quantities);
        solution.DoAlgebra();

        return solution.GetWork();
    }

    // solves an equation using water with correct unknowns
    public static Steps SolveWater(string[] quantities) {

        ConvertUnitsInArray(quantities);

        //construct an array that tells us which ones are known
        if (quantities.Length != 5) {
            throw new System.ArgumentException("invalid set of quantites");
        }

        bool[] knowns = ConstructKnowns(quantities);

        Water solution = new Water(knowns, quantities);
        solution.DoAlgebra();

        return solution.GetWork();

    }

    // solves an equation using fire with correct unknowns
    public static Steps SolveFire(string[] quantities) {

        ConvertUnitsInArray(quantities);

        //construct an array that tells us which ones are known
        if (quantities.Length != 5) {
            throw new System.ArgumentException("invalid set of quantites");
        }

        bool[] knowns = ConstructKnowns(quantities);

        Fire solution = new Fire(knowns, quantities);
        solution.DoAlgebra();

        return solution.GetWork();

    }

    // solves an equation using shadow with correct unknowns
    public static Steps SolveShadow(string[] quantities) {

        ConvertUnitsInArray(quantities);

        //construct an array that tells us which ones are known
        if (quantities.Length != 5) {
            throw new System.ArgumentException("invalid set of quantites");
        }

        bool[] knowns = ConstructKnowns(quantities);

        Shadow solution = new Shadow(knowns, quantities);
        solution.DoAlgebra();

        return solution.GetWork();

    }

    // solves an equation using air with correct unknowns
    public static Steps SolveAir(string[] quantities) {

        ConvertUnitsInArray(quantities);

        //construct an array that tells us which ones are known
        if (quantities.Length != 5) {
            throw new System.ArgumentException("invalid set of quantites");
        }

        bool[] knowns = ConstructKnowns(quantities);

        Air solution = new Air(knowns, quantities);
        solution.DoAlgebra();

        return solution.GetWork();

    }

    public static bool CheckValidity(string[] quantities) {

        float[] numberQuantities = new float[5];

        for(int i = 0; i < 5; i ++) {
            numberQuantities[i] = float.Parse(quantities[i]);
        }

        int missingIndex = GetMissingIndex(quantities);

        if(missingIndex == 0) {
            //Shadow
            return Algebra.IsEqualFloat(numberQuantities[4], numberQuantities[1] - (0.5f * numberQuantities[3] * Mathf.Pow(numberQuantities[2], 2)));
        }
        else if(missingIndex == 1) {
            //Air
            return Algebra.IsEqualFloat(numberQuantities[4], numberQuantities[0] + (0.5f * numberQuantities[3] * Mathf.Pow(numberQuantities[2], 2)));
        }
        else if(missingIndex == 2) {
            //Fire
            return Algebra.IsEqualFloat(Mathf.Pow(numberQuantities[1], 2), Mathf.Pow(numberQuantities[0], 2) + (2f * numberQuantities[3] * numberQuantities[4]));
        }
        else if(missingIndex == 3) {
            //Water
            return Algebra.IsEqualFloat(numberQuantities[4], 0.5f * numberQuantities[2] * (numberQuantities[0] + numberQuantities[1]));
        }
        else if(missingIndex == 4) {
            //Earth
            return Algebra.IsEqualFloat(numberQuantities[1], numberQuantities[0] + (0.5f * numberQuantities[3] * numberQuantities[2]));
        }
        else {
            throw new System.ArgumentException();
        }
    }

    //** PRIVATE METHODS **\\

    // given a string[] of quantities, constructs a bool[] that
    // can be passed to subclasses of KinematicEquation
    private static bool[] ConstructKnowns(string[] quantities) {
        bool[] result = new bool[5];

        for (int i = 0; i < 5; i++) {
            result[i] = !(quantities[i].Equals(emptyVariableSet[i]));
        }

        return result;
    }

     private static bool know3SharedValues(bool[] knowns1, bool[] knowns2) {
        // Assuming that knowns1.length = knowns2.length = 5
        int count = 0;
        for (int index = 0; index < knowns1.Length; index++) {
            if (knowns1[index] && knowns2[index]) {
                count++;
            }
        }

        return count == 3;
    }

   private static int GetUnkownSharedIndex(bool[] inputKnowns, bool[] equationKnowns) {
        for (int index = 0; index < inputKnowns.Length; index++) {
            if (inputKnowns[index] == false && equationKnowns[index]) {
                return index;
            }
        }
        throw new System.ArgumentException("ERROR: No index exists in which equation knowns and input knowns contradict");
    }

    private static void ConvertUnitsInArray(string[] array) {
        for(int i = 0; i < array.Length; i ++) {
            string[] terms = array[i].Split(' ');
            if (terms.Length == 2) {
                if (i == 0) {
                    array[i] = (float.Parse(terms[0]) * UnitConversion.UnitToConversionFactor(terms[1], 1)).ToString();
                }
                else {
                    array[i] = (float.Parse(terms[0]) * UnitConversion.UnitToConversionFactor(terms[1], i)).ToString();
                }
            }
        }
    }

    private static int GetMissingIndex(string[] quantities) {
        foreach(string a in quantities) {
            Debug.Log(a);
        }

        for(int i = 0; i  < 5; i ++) {
            if(quantities[i].Equals(emptyVariableSet[i])) {
                return i;
            }
        }

        throw new System.ArgumentException();
    }
}