using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNote.Models
{
    public interface IEntityModel
    {
        /// <summary>
        /// Model id for database
        /// </summary>
        int Id { get; set; }
    }
}
