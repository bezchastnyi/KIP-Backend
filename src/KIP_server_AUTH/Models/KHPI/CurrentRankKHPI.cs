// <copyright file="CurrentRankKHPI.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_server_AUTH.Models.KHPI
{
    /// <summary>
    /// Current Rank KHPI.
    /// </summary>
    public class CurrentRankKHPI
    {
        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>rank.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string n { get; set; }

        /// <summary>
        /// Gets or sets the id of student.
        /// </summary>
        /// <value>id of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string studid { get; set; }

        /// <summary>
        /// Gets or sets the fio of student.
        /// </summary>
        /// <value>fio of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string fio { get; set; }

        /// <summary>
        /// Gets or sets the group of student.
        /// </summary>
        /// <value>group of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string group { get; set; }

        /// <summary>
        /// Gets or sets the full form of rank mark.
        /// </summary>
        /// <value>full form of rank mark.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string sbal100 { get; set; }

        /// <summary>
        /// Gets or sets the short form of rank mark.
        /// </summary>
        /// <value>short form of rank mark.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string sbal5 { get; set; }

        /// <summary>
        /// Gets or sets the formula of rank calculating.
        /// </summary>
        /// <value>formula of rank calculating.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string rating { get; set; }
    }
}
