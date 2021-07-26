// <copyright file="Building.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_Backend.Models.KIP
{
    /// <summary>
    /// Buildings KIP.
    /// </summary>
    public class Building
    {
        /// <summary>
        /// Gets or sets the id of buildings.
        /// </summary>
        public int BuildingID { get; set; }

        /// <summary>
        /// Gets or sets the full name of buildings.
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// Gets or sets the short name of buildings.
        /// </summary>
        public string BuildingShortName { get; set; }
    }
}
