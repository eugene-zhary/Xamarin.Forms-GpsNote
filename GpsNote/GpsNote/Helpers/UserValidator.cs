namespace GpsNote.Helpers
{
    public static class UserValidator
    {
        public static bool Validate(string email, string password) => StringValidators.ValidateEmail(email)
            && StringValidators.ValidatePassword(password);

        public static bool Validate(string email, string password, string name) => StringValidators.ValidateEmail(email)
            && StringValidators.ValidatePassword(password)
            && StringValidators.ValidateName(name);
    }
}
