// <copyright file="PersonalInformationKhPI.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace KIP_server_Auth.Models.KhPI
{
    /// <summary>
    /// Personal Information KhPI.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PersonalInformationKhPI
    {
        /// <summary>
        /// Gets or sets the id of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string st_cod { get; set; }

        /// <summary>
        /// Gets or sets the last name of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string fam { get; set; }

        /// <summary>
        /// Gets or sets the first name of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string imya { get; set; }

        /// <summary>
        /// Gets or sets the patronymic of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string otch { get; set; }

        /// <summary>
        /// Gets or sets the course of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string kurs { get; set; }

        /// <summary>
        /// Gets or sets the group of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string grupa { get; set; }

        /// <summary>
        /// Gets or sets the faculty of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string fakultet { get; set; }

        /// <summary>
        /// Gets or sets the cathedra of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string kafedra { get; set; }

        /// <summary>
        /// Gets or sets the specialization of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string specialization { get; set; }

        /// <summary>
        /// Gets or sets the speciality of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string speciality { get; set; }

        /// <summary>
        /// Gets or sets the studying program of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string osvitprog { get; set; }

        /// <summary>
        /// Gets or sets the studying level of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string train_level { get; set; }

        /// <summary>
        /// Gets or sets the studying form of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string train_form { get; set; }

        /// <summary>
        /// Gets or sets the budjet form of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string oplata { get; set; }

        /// <summary>
        /// Gets or sets the group id of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string gid { get; set; }

        /// <summary>
        /// Gets or sets the faculty id of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string fid { get; set; }

        /// <summary>
        /// Gets or sets the cathedra id of student.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string kid { get; set; }
    }
}
