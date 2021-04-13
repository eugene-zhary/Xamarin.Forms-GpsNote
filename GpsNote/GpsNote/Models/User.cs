using SQLite;

namespace GpsNote.Models
{
    [Table("Users")]
    public class User : IEntityModel
    {
        public User()
        {

        }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        #region -- Public properties --

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [Unique]
        public string Email { get; set; }
        public string Password { get; set; }

        #endregion
    }
}
