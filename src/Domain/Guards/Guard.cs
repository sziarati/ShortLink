using Domain.Exceptions;
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

        public static void AgainstInvalidEmail(string email, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidEmailException(nameof(parameterName));
            }

            if (!EmailRegex.IsMatch(email))
            {
                throw new InvalidEmailException(nameof(parameterName));
            }
        }
        public static void AgainstInvalidPassword(string password, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidPasswordException(nameof(parameterName));
            }

            if (!PasswordRegex.IsMatch(password))
            {
                throw new InvalidPasswordException(nameof(parameterName));
            }
        }
        public static void AgainstInvalidPostalCode(string postalCode, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                throw new InvalidPostalCodeException(nameof(parameterName));
            }

            if (!PostalCodeRegex.IsMatch(postalCode))
            {
                throw new InvalidPostalCodeException(nameof(parameterName));
            }
        }
    }
}
