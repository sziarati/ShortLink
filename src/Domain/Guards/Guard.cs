using Domain.Exceptions;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Domain.Guards
{
    public static class Guard
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex PasswordRegex = new Regex(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex PostalCodeRegex = new Regex(
            @"^\d{10}$", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static void AgainstInvalidEmail(string email, [CallerMemberName] string memberName = "")
        {
            if (string.IsNullOrWhiteSpace(email) || !EmailRegex.IsMatch(email))
            {
                throw new InvalidEmailException(email);
            }
        }
        public static void AgainstInvalidPassword(string password, [CallerMemberName]  string memberName = "")
        {
            if (string.IsNullOrWhiteSpace(password) || !PasswordRegex.IsMatch(password))
            {
                throw new InvalidPasswordException();
            }
        }
        public static void AgainstInvalidPostalCode(string postalCode, [CallerMemberName] string memberName = "")
        {
            if (string.IsNullOrWhiteSpace(postalCode) || !PostalCodeRegex.IsMatch(postalCode))
            {
                throw new InvalidPostalCodeException(postalCode);
            }
        }
    }
}
