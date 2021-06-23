using GpsNote.Interfaces;
using SQLite;

namespace GpsNote.Models
{
    [Table(Constants.Database.PINS_TABLE_NAME)]
    public class PinModel : IEntityModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Label { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool IsFavorite { get; set; }

        public int UserId { get; set; }
    }
}
