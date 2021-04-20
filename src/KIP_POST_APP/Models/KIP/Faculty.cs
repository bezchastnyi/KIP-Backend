// <copyright file="Faculty.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_POST_APP.Models.KIP
{
    /// <summary>
    /// Faculties KIP.
    /// </summary>
    public class Faculty
    {
        /// <summary>
        /// Gets or sets the id of faculties.
        /// </summary>
        /// <value>ID of faculty.</value>
        [Key]
        [Index]
        [Required(ErrorMessage = "FacultyID is required")]
        public int FacultyID { get; set; }

        /// <summary>
        /// Gets or sets the full name of faculties.
        /// </summary>
        /// <value>Full name of faculty.</value>
        [Required(ErrorMessage = "FacultyName is required")]
        [Column(TypeName = "varchar(100)")]
        public string FacultyName { get; set; }

        /// <summary>
        /// Gets or sets the short name of faculties.
        /// </summary>
        /// <value>Short name of faculty.</value>
        [Column(TypeName = "varchar(7)")]
        public string FacultyShortName { get; set; }
    }
}
