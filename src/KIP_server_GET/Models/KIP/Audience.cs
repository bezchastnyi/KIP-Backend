// <copyright file="Audience.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_server_GET.Models.KIP
{
    /// <summary>
    /// Audiences KIP.
    /// </summary>
    public class Audience
    {
        /// <summary>
        /// Gets or sets the id of audiences.
        /// </summary>
        /// <value>ID of audiences.</value>
        [Key]
        [Index]
        [Required(ErrorMessage = "AudienceID is required")]
        public int AudienceID { get; set; }

        /// <summary>
        /// Gets or sets the name of audiences.
        /// </summary>
        /// <value>Name of audiences.</value>
        [Required(ErrorMessage = "AudienceName is required")]
        [Column(TypeName = "varchar(100)")]
        public string AudienceName { get; set; }

        /// <summary>
        /// Gets or sets the number of seats in audiences.
        /// </summary>
        /// <value>Number of seats in audiences.</value>
        [Range(0, 1000, ErrorMessage = "NumberOfSeats must be between 0 and 1000")]
        public int? NumberOfSeats { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Schedule Is Present for the current group.
        /// </summary>
        /// <value>Course of group.</value>
        public List<bool> ScheduleIsPresent { get; set; } = new List<bool>()
        {
            false,
            false,
            false,
            false,
            false,
            false,
        };

        /// <summary>
        /// Gets or sets the id of building.
        /// </summary>
        /// <value>ID of audiences.</value>
        [Required(ErrorMessage = "BuildingID is required")]
        public int BuildingID { get; set; }

        /// <summary>
        /// Gets or sets the building.
        /// </summary>
        /// <value>Building.</value>
        [ForeignKey("BuildingID")]
        public Building Building { get; set; }
    }
}
