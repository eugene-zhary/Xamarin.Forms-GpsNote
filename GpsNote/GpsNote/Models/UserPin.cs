using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace GpsNote.Models
{
    [Table("Pins")]
    public class UserPin : IEntityModel
    {
        public UserPin() {
            IconSource = "ic_common.png";
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Label { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public bool IsFavorite { get; set; }
        public string IconSource { get; set; }
        public int UserId { get; set; }

    }
}
