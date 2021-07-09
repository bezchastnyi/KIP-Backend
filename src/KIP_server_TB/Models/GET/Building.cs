// <copyright file="Building.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_server_TB.Models.GET
{
    /// <summary>
    /// Buildings KIP.
    /// </summary>
    public class Building
    {
        /// <summary>
        /// Gets or sets the id of buildings.
        /// </summary>
        /// <value>ID of building.</value>
        public int BuildingID { get; set; }

        /// <summary>
        /// Gets or sets the full name of buildings.
        /// </summary>
        /// <value>Full name of building.</value>
        public string BuildingName { get; set; }

        /// <summary>
        /// Gets or sets the short name of buildings.
        /// </summary>
        /// <value>Short name of buildings.</value>
        public string BuildingShortName { get; set; }
    }
}
