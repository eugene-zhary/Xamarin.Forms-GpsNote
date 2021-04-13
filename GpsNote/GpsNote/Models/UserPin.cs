using SQLite;

namespace GpsNote.Models
{
    [Table("Pins")]
    public class UserPin : IEntityModel
    {
        #region -- Public properties --

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Label { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public bool IsFavorite { get; set; }
        public int UserId { get; set; }

        #endregion
    }
}
