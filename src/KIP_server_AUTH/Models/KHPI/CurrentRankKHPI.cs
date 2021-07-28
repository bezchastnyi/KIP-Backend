// <copyright file="CurrentRankKhPI.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace KIP_server_Auth.Models.KhPI
{
    /// <summary>
    /// Current Rank KhPI.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CurrentRankKhPI
    {
        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string n { get; set; }

        /// <summary>
        /// Gets or sets the id of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string studid { get; set; }

        /// <summary>
        /// Gets or sets the fio of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string fio { get; set; }

        /// <summary>
        /// Gets or sets the group of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string group { get; set; }

        /// <summary>
        /// Gets or sets the full form of rank mark.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string sbal100 { get; set; }

        /// <summary>
        /// Gets or sets the short form of rank mark.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string sbal5 { get; set; }

        /// <summary>
        /// Gets or sets the formula of rank calculating.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string rating { get; set; }
    }
}
