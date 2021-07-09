// <copyright file="Audience.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace KIP_server_TB.Models.GET
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
        public int AudienceID { get; set; }

        /// <summary>
        /// Gets or sets the name of audiences.
        /// </summary>
        /// <value>Name of audiences.</value>
        public string AudienceName { get; set; }

        /// <summary>
        /// Gets or sets the number of seats in audiences.
        /// </summary>
        /// <value>Number of seats in audiences.</value>
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
        public int BuildingID { get; set; }

        /// <summary>
        /// Gets or sets the building.
        /// </summary>
        /// <value>Building.</value>
        public Building Building { get; set; }
    }
}
