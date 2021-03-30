using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNote.Models
{
    [Table("Users")]
    public class User : IEntityModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
