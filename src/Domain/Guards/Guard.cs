using Domain.Results;
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

        public static Result<string> AgainstInvalidEmail(string email, [CallerMemberName] string memberName = "")
        {
            if (string.IsNullOrWhiteSpace(email) || !EmailRegex.IsMatch(email))
            {
                return Result<string>.Failure(DomainErrors.InvalidEmailError, $"{memberName} with value {email} in inValid");
            }

            return Result<string>.Success(email);
        }
        public static Result<string> AgainstInvalidPassword(string password, [CallerMemberName] string memberName = "")
        {
            if (string.IsNullOrWhiteSpace(password) || !PasswordRegex.IsMatch(password))
            {
                return Result<string>.Failure(DomainErrors.InvalidPasswordError, $"{memberName} with value {password} in inValid");
            }

            return Result<string>.Success(password);
        }
        public static Result<string> AgainstInvalidPostalCode(string postalCode, [CallerMemberName] string memberName = "")
        {
            if (string.IsNullOrWhiteSpace(postalCode) || !PostalCodeRegex.IsMatch(postalCode))
            {
                return Result<string>.Failure(DomainErrors.InvalidPostalCodeError, $"{memberName} with value {postalCode} in inValid");
            }

            return Result<string>.Success(postalCode);
        }
    }
}
