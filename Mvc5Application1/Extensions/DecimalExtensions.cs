namespace Mvc5Application1.Extensions
{
    public static class DecimalFormat
    {
        public static string Format(this decimal? number, string format)
        {
            return number.HasValue ? number.Value.ToString(format) : string.Empty;
        }

        public static string GetQuantityFormat(this string uom, string cinifactor = "")
        {
            if (cinifactor != "")
            {
                if (uom == "EA")
                {
                    return "N3";
                }
            }
            switch (uom)
            {
                case "LM":
                    return "N3";

                case "EA":
                    return "N0";

                default: return "N2";
            }
        }
    }
}