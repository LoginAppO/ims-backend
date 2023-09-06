using System.Text.RegularExpressions;

namespace Ims.Infrastructure.ExtensionMethods;

public static class StringExtensions
{
    public static string NormalizeAndConsolidateWhitespace(this string input)
    {
        return Regex.Replace(input.Trim().ToLower(), @"\s+", " ");
    }
}
