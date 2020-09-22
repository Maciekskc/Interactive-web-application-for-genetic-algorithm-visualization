using System.Text.RegularExpressions;
using Application.Infrastructure;

namespace Application.Utilities
{
    public static class Extensions
    {
        public static bool IsValidEmail(this string str)
        {
            Regex regex = new Regex(Constants.CheckEmailRegex);
            Match match = regex.Match(str);
            if (match.Success)
                return true;

            return false;
        }

        public static bool IsValidPassword(this string str)
        {
            Regex regex = new Regex(Constants.CheckPasswordRegex);
            Match match = regex.Match(str);
            if (match.Success)
                return true;

            return false;
        }
    }
}