using System.Text.RegularExpressions;

namespace GpsNote.Helpers
{
    public static class StringValidators
    {
        private const string EMAIL_REG = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        private const string NAME_REG = @"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$";

        private const string PASSWORD_REG = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$";

        public static bool ValidateEmail(string email) => Regex.IsMatch(email, EMAIL_REG);

        public static bool ValidateName(string name) => Regex.IsMatch(name, NAME_REG);

        public static bool ValidatePassword(string password) => Regex.IsMatch(password, PASSWORD_REG);
    }
}
