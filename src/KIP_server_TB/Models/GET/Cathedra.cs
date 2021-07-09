// <copyright file="Cathedra.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_server_TB.Models.GET
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
        public int CathedraID { get; set; }

        /// <summary>
        /// Gets or sets the full name of departments.
        /// </summary>
        /// <value>Full name of departments.</value>
        public string CathedraName { get; set; }

        /// <summary>
        /// Gets or sets the short name of departments.
        /// </summary>
        /// <value>Short name of departments.</value>
        public string CathedraShortName { get; set; }

        /// <summary>
        /// Gets or sets the id of faculty.
        /// </summary>
        /// <value>ID of faculty.</value>
        public int FacultyID { get; set; }

        /// <summary>
        /// Gets or sets the faculty.
        /// </summary>
        /// <value>Faculty.</value>
        /// [ForeignKey("FacultyID")]
        public Faculty Faculty { get; set; }
    }
}
