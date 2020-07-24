using UnityEngine;

public class UnitConversion {
    private UnitConversion displacementConversion;
    private UnitConversion timeConversion;

    private static readonly string[] validTimeUnits =
        {"ps", "ms", "jiffy", "s", "sec", "min", "h", "hr", "hrs", "day", "wk",
        "ftn", "mo", "yr"};
    private static readonly float[] convertToSeconds = { 1E-12f, 0.001f, 0.01f, 1f,
        1f, 60f, 3600f, 3600f, 3600f, 86400f, 604800f, 1209600f, 2.628E6f, 3.154E7f };
    private static readonly string[] validDisplacementUnits = {"in", "ft", "yd",
        "ftbl", "fm", "cbl", "fur", "mi", "nmi", "nm", "mm", "cm", "m", "km",
        "au", "lyr"};
    private static readonly float[] convertToMeters = { 0.0254f, 0.3048f, 0.9144f,
        91.44f, 1.8288f, 185.2f, 201.168f, 1609.34f, 1852, 1E-9f, 0.001f, 0.01f, 1, 1000,
        1.496E11f, 9.461E15f };

    private int conversionIndex;

    //prevents instantiation
    private UnitConversion() { }

    //takes ANY unit and finds a conversion factor to base units
    public static float UnitToConversionFactor(string unit, int quantityIndex) {

        if (quantityIndex != GetQuantityIndex(unit)) {
            throw new System.ArgumentException("ERROR: Invalid unit: " + unit);
        }

        //if singular unit, skip the dimensional analysis
        if (!unit.Contains("/")) {
            return SingularUnitToConversionFactor(unit);
        }

        //essentially does dimensional analysis
        float numerator = 1;
        float denominator = 1;
        bool denominatorsquared = false; //TODO : add a bool numeratorSquared for later equations (not necessary for kinematics)

        string numeratorUnit = unit.Substring(0, unit.IndexOf('/'));
        string denominatorUnit = unit.Substring(unit.IndexOf('/') + 1);

        //if unit is acceleration
        if (denominatorUnit.Contains("^2")) {
            denominatorsquared = true;
            denominatorUnit = denominatorUnit.Substring(0, denominatorUnit.Length - 2);
        }

        //actually setting the conversion factors
        numerator = SingularUnitToConversionFactor(numeratorUnit);
        denominator = SingularUnitToConversionFactor(denominatorUnit);

        //acts accordingly if unit is acceleration
        if (denominatorsquared) {
            denominator = Mathf.Pow(denominator, 2);
        }

        return numerator / denominator;
    }

    //method will only work on singular units of displacement or time
    private static float SingularUnitToConversionFactor(string unit) {
        if (StringArrayContains(unit, validTimeUnits)) {
            return convertToSeconds[StringArrayIndexOf(unit, validTimeUnits)];
        }
        else if (StringArrayContains(unit, validDisplacementUnits)) {
            return convertToMeters[StringArrayIndexOf(unit, validDisplacementUnits)];
        }
        throw new System.ArgumentException("not (yet) a valid unit: " + unit);
    }

    public string GetUnits(string fullText) {
        return fullText.Substring(fullText.IndexOf(" ") + 1);
    }

    //some basic array methods that are for utility:

    //takes an array and checks if it contains an item (only works for String[])
    private static bool StringArrayContains(string s, string[] array) {
        foreach (string item in array) {
            if (item.Equals(s)) {
                return true;
            }
        }
        return false;
    }

    //finds the first index of a string in an array
    private static int StringArrayIndexOf(string s, string[] array) {
        for (int i = 0; i < array.Length; i++) {
            if (s.Equals(array[i])) {
                return i;
            }
        }
        return -1;
    }

    // returns whether or not the given unit is valid acceleration, velocity, time, or displacement unit
    // Velocity = 1, Time = 2, Acceleration = 3, Displacement = 4
    public static int GetQuantityIndex(string unit) {
        if (unit.Contains("/")) {
            string[] fraction = unit.Split('/');
            if (GetQuantityIndex(fraction[0]) == 4 && GetQuantityIndex(fraction[1]) == 2) {
                return 1;
            }
            else if (GetQuantityIndex(fraction[0]) == 4 && GetQuantityIndex(fraction[1]) == 3) {
                return 3;
            }
            else {
                throw new System.ArgumentException("ERROR: Invalid unit: " + unit);
            }
        }
        else {
            if (StringArrayContains(unit, validDisplacementUnits)) {
                return 4; // isDisplacement
            }
            else if (StringArrayContains(unit, validTimeUnits)) {
                return 2; // isTime
            }
            else if (StringArrayContains(unit.Substring(0, unit.IndexOf("^2")), validTimeUnits)) {
                return 3; // isAcceleration
            }
            else {
                throw new System.ArgumentException("ERROR: Invalid unit: " + unit);
            }
        }
    }

    //returns a table of possible units for the user
    public static string GetValidUnits() {
        string[,] validDisplacementUnits = { {"inches", "in"},
                                              {"feet", "ft"},
                                              {"yards", "yd"},
                                              {"football fields", "ftbl"},
                                              {"fathoms", "fm"},
                                              {"cable length", "cbl"},
                                              {"furlongs", "fur"},
                                              {"miles", "mi"},
                                              {"nautical miles", "nmi"},
                                              {"nanometers", "nm"},
                                              {"millimeters", "mm"},
                                              {"centimeters", "cm"},
                                              {"meters", "m"},
                                              {"kilometers", "km"},
                                              {"astronomical units", "au"},
                                              {"light years", "lyr"} };
        string[,] validTimeUnits = { {"picoseconds", "ps"},
                                      {"milliseconds", "ms"},
                                      {"jiffies", "jiffy"},
                                      {"seconds", "s or sec"},
                                      {"minutes", "min"},
                                      {"hours", "h or hr(s)"},
                                      {"days", "day"},
                                      {"weeks", "wk"},
                                      {"fortnights", "ftn"},
                                      {"months", "mo"},
                                      {"years", "yr"} };

        string validUnits = "";
        validUnits += "Valid Displacement Units:" + "\n";
        for (int row = 0; row < validDisplacementUnits.Length; row++) {
            validUnits += "\t" + validDisplacementUnits[row,0] + " - " + validDisplacementUnits[row,1] + "\n";
        }
        validUnits += "Valid Time Units:" + "\n";
        for (int row = 0; row < validTimeUnits.Length; row++) {
            validUnits += "\t" + validTimeUnits[row,0] + " - " + validTimeUnits[row,1] + "\n";
        }
        return validUnits;
    }
}
