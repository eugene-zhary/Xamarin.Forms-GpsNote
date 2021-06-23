using GpsNote.Interfaces;
using SQLite;

namespace GpsNote.Models
{
    [Table(Constants.Database.USERS_TABLE_NAME)]
    public class UserModel : IEntityModel
    {
        public UserModel() { }

        public UserModel(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        [Unique]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
