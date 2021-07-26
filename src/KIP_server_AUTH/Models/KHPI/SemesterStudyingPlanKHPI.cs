// <copyright file="SemesterStudyingPlanKHPI.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace KIP_server_Auth.Models.KHPI
{
    /// <summary>
    /// Semester Studying Plan KHPI.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SemesterStudyingPlanKHPI
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
        /// Gets or sets the course.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string kurs { get; set; }

        /// <summary>
        /// Gets or sets the semester.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string semestr { get; set; }

        /// <summary>
        /// Gets or sets the credits.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string credit { get; set; }

        /// <summary>
        /// Gets or sets the audits.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string audit { get; set; }

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
    }
}
