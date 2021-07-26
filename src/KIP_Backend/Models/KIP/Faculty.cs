// <copyright file="Faculty.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_Backend.Models.KIP
{
    /// <summary>
    /// Faculties KIP.
    /// </summary>
    public class Faculty
    {
        /// <summary>
        /// Gets or sets the id of faculties.
        /// </summary>
        public int FacultyID { get; set; }

        /// <summary>
        /// Gets or sets the full name of faculties.
        /// </summary>
        public string FacultyName { get; set; }

        /// <summary>
        /// Gets or sets the short name of faculties.
        /// </summary>
        public string FacultyShortName { get; set; }
    }
}
