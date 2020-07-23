
public class UnitConversion {
    private UnitConversion displacementConversion;
    private UnitConversion timeConversion;

    private static readonly string[] validTimeUnits =
        {"ps", "ms", "jiffy", "s", "sec", "min", "h", "hr", "hrs", "day", "wk",
        "ftn", "mo", "yr"};
    private static readonly float[] convertToSeconds = { 1E-12, 0.001, 0.01, 1,
        1, 60, 3600, 3600, 3600, 86400, 604800, 1209600, 2.628E6, 3.154E7 };
    private static readonly string[] validDisplacementUnits = {"in", "ft", "yd",
        "ftbl", "fm", "cbl", "fur", "mi", "nmi", "nm", "mm", "cm", "m", "km",
        "au", "lyr"};
    private static readonly float[] convertToMeters = { 0.0254, 0.3048, 0.9144,
        91.44, 1.8288, 185.2, 201.168, 1609.34, 1852, 1E-9, 0.001, 0.01, 1, 1000,
        1.496E11, 9.461E15 };

    private int conversionIndex;

    //prevents instantiation
    private UnitConversion() { }


}
