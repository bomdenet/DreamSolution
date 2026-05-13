using System.Globalization;

namespace Dream;

public static class StringExtensions
{
    public static int ToInt(this string input) => int.Parse(input, NumberStyles.Any, CultureInfo.InvariantCulture);
    public static float ToFloat(this string input) => float.Parse(input, NumberStyles.Any, CultureInfo.InvariantCulture);
    public static double ToDouble(this string input) => double.Parse(input, NumberStyles.Any, CultureInfo.InvariantCulture);
    public static decimal ToDecimal(this string input) => decimal.Parse(input, NumberStyles.Any, CultureInfo.InvariantCulture);

    public static bool TryToInt(this string input, out int result) => int.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
    public static bool TryToFloat(this string input, out float result) => float.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
    public static bool TryToDouble(this string input, out double result) => double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
    public static bool TryToDecimal(this string input, out decimal result) => decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out result);

    public static string UriCombine(this string baseUri, params string[] segments)
    {
        string combined = baseUri.TrimEnd('/', '\\');
        foreach (string part in segments)
        {
            if (string.IsNullOrEmpty(part)) continue;
            combined = $"{combined}/{part.Trim('/', '\\')}";
        }
        return combined;
    }
}
