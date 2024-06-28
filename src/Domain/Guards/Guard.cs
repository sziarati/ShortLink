﻿using Domain.Entities.ValueObjects;
using Domain.Exceptions;
using Domain.Results;
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

        public static Result<string> AgainstInvalidEmail(string email, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<string>.Failure(DomainErrors.InvalidEmailError, email);
            }

            if (!EmailRegex.IsMatch(email))
            {
                return Result<string>.Failure(DomainErrors.InvalidEmailError, email);
            }

            return Result<string>.Success(email);
        }
        public static Result<string> AgainstInvalidPassword(string password, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return Result<string>.Failure(DomainErrors.InvalidPasswordError, password);
            }

            if (!PasswordRegex.IsMatch(password))
            {
                return Result<string>.Failure(DomainErrors.InvalidPasswordError, password);
            }
            
            return Result<string>.Success(password);
        }
        public static Result<string> AgainstInvalidPostalCode(string postalCode, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                return Result<string>.Failure(DomainErrors.InvalidPostalCodeError, postalCode);
            }

            if (!PostalCodeRegex.IsMatch(postalCode))
            {
                return Result<string>.Failure(DomainErrors.InvalidPostalCodeError, postalCode);
            }
            return Result<string>.Success(postalCode);
        }
    }
}
