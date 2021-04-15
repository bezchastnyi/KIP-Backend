// <copyright file="Cathedra.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_POST_APP.Models.KIP
{
    /// <summary>
    /// Departments KIP.
    /// </summary>
    public class Cathedra
    {
        /// <summary>
        /// Gets or sets the id of departments.
        /// </summary>
        /// <value>ID of departments.</value>
        [Key]
        [Index]
        [Required(ErrorMessage = "CathedraID is required")]
        public int CathedraID { get; set; }

        /// <summary>
        /// Gets or sets the full name of departments.
        /// </summary>
        /// <value>Full name of departments.</value>
        [Required(ErrorMessage = "CathedraName is required")]
        [Column(TypeName = "varchar(100)")]
        public string CathedraName { get; set; }

        /// <summary>
        /// Gets or sets the short name of departments.
        /// </summary>
        /// <value>Short name of departments.</value>
        [Column(TypeName = "varchar(7)")]
        public string CathedraShortName { get; set; }

        /// <summary>
        /// Gets or sets the id of faculty.
        /// </summary>
        /// <value>ID of faculty.</value>
        [Index]
        [Required(ErrorMessage = "FacultyID is required")]
        public int FacultyID { get; set; }

        /// <summary>
        /// Gets or sets the faculty.
        /// </summary>
        /// <value>Faculty.</value>
        /// [ForeignKey("FacultyID")]
        public Faculty Faculty { get; set; }
    }
}
