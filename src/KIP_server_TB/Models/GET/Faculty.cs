// <copyright file="Faculty.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_server_TB.Models.GET
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
        public int FacultyID { get; set; }

        /// <summary>
        /// Gets or sets the full name of faculties.
        /// </summary>
        /// <value>Full name of faculty.</value>
        public string FacultyName { get; set; }

        /// <summary>
        /// Gets or sets the short name of faculties.
        /// </summary>
        /// <value>Short name of faculty.</value>
        public string FacultyShortName { get; set; }
    }
}
