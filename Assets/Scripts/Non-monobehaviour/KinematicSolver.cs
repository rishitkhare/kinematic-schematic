public class KinematicSolver {

    // when passing a string[] of quantities to any of the solve methods, the unknowns should match this list
    public static readonly string[] emptyVariableSet = {"Vi", "Vf", "Δt", "a", "ΔX"};

    // prevents instantiation
    private KinematicSolver() { }


    // solves an equation with 3 or more unknowns
    public static string SolveGeneral(string[] quantities) {

        ConvertUnitsInArray(quantities);

        bool[] knowns = ConstructKnowns(quantities);
        if (know3SharedValues(knowns, Earth.presentQuantities)) {
            string answer = SolveEarth(quantities);
            quantities[GetUnkownSharedIndex(knowns, Earth.presentQuantities)] = answer.Substring(answer.IndexOf("=") + 2);
            return answer;
        }
        else if (know3SharedValues(knowns, Water.presentQuantities)) {
            string answer = SolveWater(quantities);
            quantities[GetUnkownSharedIndex(knowns, Water.presentQuantities)] = answer.Substring(answer.IndexOf("=") + 2);
            return answer;
        }
        else if (know3SharedValues(knowns, Fire.presentQuantities)) {
            string answer = SolveFire(quantities);
            quantities[GetUnkownSharedIndex(knowns, Fire.presentQuantities)] = answer.Substring(answer.IndexOf("=") + 2);
            return answer;
        }
        else if (know3SharedValues(knowns, Air.presentQuantities)) {
            string answer = SolveAir(quantities);
            quantities[GetUnkownSharedIndex(knowns, Air.presentQuantities)] = answer.Substring(answer.IndexOf("=") + 2);
            return answer;
        }
        else if (know3SharedValues(knowns, Shadow.presentQuantities)) {
            string answer = SolveShadow(quantities);
            quantities[GetUnkownSharedIndex(knowns, Shadow.presentQuantities)] = answer.Substring(answer.IndexOf("=") + 2);
            return answer;
        }
        else {
            throw new System.ArgumentException("ERROR: HOW DID WE GET HERE.");
        }  
    }

    // solves an equation using earth with correct unknowns
    public static string SolveEarth(string[] quantities) {

        ConvertUnitsInArray(quantities);

        //construct an array that tells us which ones are known
        if (quantities.Length != 5) {
            throw new System.ArgumentException("invalid set of quantites");
        }

        bool[] knowns = ConstructKnowns(quantities);

        Earth solution = new Earth(knowns, quantities);
        solution.DoAlgebra();

        return solution.GetAnswer();
    }

    // solves an equation using water with correct unknowns
    public static string SolveWater(string[] quantities) {

        ConvertUnitsInArray(quantities);

        //construct an array that tells us which ones are known
        if (quantities.Length != 5) {
            throw new System.ArgumentException("invalid set of quantites");
        }

        bool[] knowns = ConstructKnowns(quantities);

        Water solution = new Water(knowns, quantities);
        solution.DoAlgebra();

        return solution.GetAnswer();

    }

    // solves an equation using fire with correct unknowns
    public static string SolveFire(string[] quantities) {

        ConvertUnitsInArray(quantities);

        //construct an array that tells us which ones are known
        if (quantities.Length != 5) {
            throw new System.ArgumentException("invalid set of quantites");
        }

        bool[] knowns = ConstructKnowns(quantities);

        Fire solution = new Fire(knowns, quantities);
        solution.DoAlgebra();

        return solution.GetAnswer();

    }

    // solves an equation using shadow with correct unknowns
    public static string SolveShadow(string[] quantities) {

        ConvertUnitsInArray(quantities);

        //construct an array that tells us which ones are known
        if (quantities.Length != 5) {
            throw new System.ArgumentException("invalid set of quantites");
        }

        bool[] knowns = ConstructKnowns(quantities);

        Shadow solution = new Shadow(knowns, quantities);
        solution.DoAlgebra();

        return solution.GetAnswer();

    }

    // solves an equation using air with correct unknowns
    public static string SolveAir(string[] quantities) {

        ConvertUnitsInArray(quantities);

        //construct an array that tells us which ones are known
        if (quantities.Length != 5) {
            throw new System.ArgumentException("invalid set of quantites");
        }

        bool[] knowns = ConstructKnowns(quantities);

        Air solution = new Air(knowns, quantities);
        solution.DoAlgebra();

        return solution.GetAnswer();

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
}