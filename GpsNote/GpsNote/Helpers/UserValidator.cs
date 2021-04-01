using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNote.Helpers
{
    public static class UserValidator
    {
        public static bool IsValid(string email, string password) =>
            (StringValidators.IsValidEmail(email) &&
             StringValidators.IsValidPassword(password));

        public static bool IsValid(string email, string password, string name) =>
             (StringValidators.IsValidEmail(email) &&
             StringValidators.IsValidPassword(password) &&
             (StringValidators.IsValidName(name)));
    }
}
