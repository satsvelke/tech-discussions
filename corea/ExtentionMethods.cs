// extentions methods -- methods which extends type or object 

using System.Globalization;

public static class Test
{
    public static int ToInt(this string value, int defaultValue = default)
    {
        return int.Parse(value, CultureInfo.CurrentCulture);
    }
}