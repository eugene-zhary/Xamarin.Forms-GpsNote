using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNote.Helpers
{
    public static class UserValidator
    {
        public static string ValidateUser(string email, string password, string name = null)
        {
            string output = String.Empty;

            if (!StringValidators.IsValidEmail(email))
            {
                output += "Email format isn't valid!\n";
            }
            if (!StringValidators.IsValidPassword(password))
            {
                output += "Password format isn't valid!\n";
            }
            if (name != null && !StringValidators.IsValidName(name))
            {
                output += "Name format isn't valid!\n";
            }

            return output;
        }
    }
}
