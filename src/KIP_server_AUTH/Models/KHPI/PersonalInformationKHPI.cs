// <copyright file="PersonalInformationKHPI.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_auth_mode.Models.KHPI
{
    /// <summary>
    /// Personal Information KHPI.
    /// </summary>
    public class PersonalInformationKHPI
    {
        /// <summary>
        /// Gets or sets the id of student.
        /// </summary>
        /// <value>id of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string st_cod { get; set; }

        /// <summary>
        /// Gets or sets the last name of student.
        /// </summary>
        /// <value>last name of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string fam { get; set; }

        /// <summary>
        /// Gets or sets the first name of student.
        /// </summary>
        /// <value>first name of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string imya { get; set; }

        /// <summary>
        /// Gets or sets the patronymic of student.
        /// </summary>
        /// <value>patronymic of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string otch { get; set; }

        /// <summary>
        /// Gets or sets the course of student.
        /// </summary>
        /// <value>course of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string kurs { get; set; }

        /// <summary>
        /// Gets or sets the group of student.
        /// </summary>
        /// <value>group of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string grupa { get; set; }

        /// <summary>
        /// Gets or sets the faculty of student.
        /// </summary>
        /// <value>faculty of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string fakultet { get; set; }

        /// <summary>
        /// Gets or sets the cathedra of student.
        /// </summary>
        /// <value>cathedra of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string kafedra { get; set; }

        /// <summary>
        /// Gets or sets the specialization of student.
        /// </summary>
        /// <value>specialization of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string specialization { get; set; }

        /// <summary>
        /// Gets or sets the speciality of student.
        /// </summary>
        /// <value>speciality of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string speciality { get; set; }

        /// <summary>
        /// Gets or sets the studying program of student.
        /// </summary>
        /// <value>studying program of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string osvitprog { get; set; }

        /// <summary>
        /// Gets or sets the studying level of student.
        /// </summary>
        /// <value>studying level of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string train_level { get; set; }

        /// <summary>
        /// Gets or sets the studying form of student.
        /// </summary>
        /// <value>studying form of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string train_form { get; set; }

        /// <summary>
        /// Gets or sets the budjet form of student.
        /// </summary>
        /// <value>budjet form of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string oplata { get; set; }

        /// <summary>
        /// Gets or sets the group id of student.
        /// </summary>
        /// <value>group id of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string gid { get; set; }

        /// <summary>
        /// Gets or sets the faculty id of student.
        /// </summary>
        /// <value>faculty id of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string fid { get; set; }

        /// <summary>
        /// Gets or sets the cathedra id of student.
        /// </summary>
        /// <value>cathedra id of student.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<KHPIDB>")]
        public string kid { get; set; }
    }
}
