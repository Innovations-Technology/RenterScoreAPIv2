namespace RenterScoreAPIv2.Utilities;

using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class StringExtensions
{
    public static string ToSnakeCase(this string input)
    {
        if (input.IsNullOrEmpty())
            return input;

        var result = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (char.IsUpper(c) && i > 0 && !char.IsWhiteSpace(input[i - 1]))
            {
                result.Append('_');
            }
            result.Append(char.ToLower(c));
        }
        return result.ToString();
    }
}