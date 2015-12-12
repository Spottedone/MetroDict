namespace MetroDictLib.Helpers
{
    public static class StringExtensions
    {
        public static string TruncateWithEllipsis(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + " ..";
        }
    }
}
