// <copyright file="ProfKhPI.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_server_NoAuth.Models.KhPI
{
    /// <summary>
    /// Teachers KhPI.
    /// </summary>
    public class ProfKhPI
    {
        /// <summary>
        /// Gets or sets the title of teachers.
        /// </summary>
        /// <value>title of teachers.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string title { get; set; }

        /// <summary>
        /// Gets or sets the id of teachers.
        /// </summary>
        /// <value>ID of teachers.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public int id { get; set; }
    }
}
