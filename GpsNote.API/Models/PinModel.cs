namespace GpsNote.API.Models
{
    public class PinModel : BaseEntity
    {
        public string Label { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsFavorite { get; set; }
        public int UserId { get; set; }
    }
}