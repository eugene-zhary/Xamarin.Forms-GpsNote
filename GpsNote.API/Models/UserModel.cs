namespace GpsNote.API.Models
{
    public class UserModel : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
