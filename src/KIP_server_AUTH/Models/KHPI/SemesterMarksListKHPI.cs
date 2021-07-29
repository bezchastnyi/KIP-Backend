// <copyright file="SemesterMarksListKHPI.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace KIP_server_AUTH.Models.KHPI
{
    /// <summary>
    /// Semester Marks List KHPI.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SemesterMarksListKHPI
    {
        /// <summary>
        /// Gets or sets the id of subject.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string subj_id { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string subject { get; set; }

        /// <summary>
        /// Gets or sets the prof.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string prepod { get; set; }

        /// <summary>
        /// Gets or sets the cathedra.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string kafedra { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation of cathedra.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string kabr { get; set; }

        /// <summary>
        /// Gets or sets the short mark.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string oc_short { get; set; }

        /// <summary>
        /// Gets or sets the national mark.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string oc_naz { get; set; }

        /// <summary>
        /// Gets or sets the full mark.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string oc_bol { get; set; }

        /// <summary>
        /// Gets or sets the ECTS mark.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string oc_ects { get; set; }

        /// <summary>
        /// Gets or sets the credits.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string credit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the debt is present.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string if_hvost { get; set; }

        /// <summary>
        /// Gets or sets ???.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string indzav { get; set; }

        /// <summary>
        /// Gets or sets the control type.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string control { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string data { get; set; }
    }
}
