// <copyright file="BuildingKHPI.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_server_GET.Models.KHPI
{
    /// <summary>
    /// Building KhPI.
    /// </summary>
    public class BuildingKHPI
    {
        /// <summary>
        /// Gets or sets the title of building.
        /// </summary>
        /// <value>title of building.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string title { get; set; }

        /// <summary>
        /// Gets or sets the id of building.
        /// </summary>
        /// <value>ID of building.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public int id { get; set; }
    }
}
