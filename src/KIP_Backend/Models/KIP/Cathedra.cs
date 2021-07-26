// <copyright file="Cathedra.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_Backend.Models.KIP
{
    /// <summary>
    /// Departments KIP.
    /// </summary>
    public class Cathedra
    {
        /// <summary>
        /// Gets or sets the id of departments.
        /// </summary>
        public int CathedraID { get; set; }

        /// <summary>
        /// Gets or sets the full name of departments.
        /// </summary>
        public string CathedraName { get; set; }

        /// <summary>
        /// Gets or sets the short name of departments.
        /// </summary>
        public string CathedraShortName { get; set; }

        /// <summary>
        /// Gets or sets the id of faculty.
        /// </summary>
        public int FacultyID { get; set; }
    }
}
