
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


}
