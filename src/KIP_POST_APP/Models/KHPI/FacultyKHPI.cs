﻿// <copyright file="FacultyKHPI.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_POST_APP.Models.KHPI
{
    /// <summary>
    /// Faculties KhPI.
    /// </summary>
    public class FacultyKHPI
    {
        /// <summary>
        /// Gets or sets the title of faculties.
        /// </summary>
        /// <value>title of faculties.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string title { get; set; }

        /// <summary>
        /// Gets or sets the id of faculties.
        /// </summary>
        /// <value>ID of faculties.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public int id { get; set; }
    }
}
