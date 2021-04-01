﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace GpsNote.Models
{
    [Table("Pins")]
    public class UsersPin : IEntityModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Label { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int UserId { get; set; }
    }
}
