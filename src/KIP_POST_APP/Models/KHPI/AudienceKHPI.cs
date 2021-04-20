// <copyright file="AudienceKHPI.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_POST_APP.Models.KHPI
{
    /// <summary>
    /// Audience KhPI.
    /// </summary>
    public class AudienceKHPI
    {
        /// <summary>
        /// Gets or sets the title of audience.
        /// </summary>
        /// <value>title of audience.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string title { get; set; }

        /// <summary>
        /// Gets or sets the Id of audience.
        /// </summary>
        /// <value>ID of audience.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public int id { get; set; }
    }
}
