// <copyright file="Building.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_POST_APP.Models.KIP
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
        [Key]
        [Index]
        [Required(ErrorMessage = "BuildingID is required")]
        public int BuildingID { get; set; }

        /// <summary>
        /// Gets or sets the full name of buildings.
        /// </summary>
        /// <value>Full name of building.</value>
        [Required(ErrorMessage = "BuildingName is required")]
        [Column(TypeName = "varchar(100)")]
        public string BuildingName { get; set; }

        /// <summary>
        /// Gets or sets the short name of buildings.
        /// </summary>
        /// <value>Short name of buildings.</value>
        [Column(TypeName = "varchar(5)")]
        public string BuildingShortName { get; set; }
    }
}
