// <copyright file="SemesterMarksList.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_server_AUTH.Models.KIP
{
    /// <summary>
    /// Semester Marks List KIP.
    /// </summary>
    public class SemesterMarksList
    {
        /// <summary>
        /// Gets or sets the id of subject.
        /// </summary>
        /// <value>ID of subject.</value>
        public int SubjectId { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>subject.</value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the prof.
        /// </summary>
        /// <value>prof.</value>
        public string Prof { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation of cathedra.
        /// </summary>
        /// <value>abbreviation of cathedra.</value>
        public string ShortCathedra { get; set; }

        /// <summary>
        /// Gets or sets the cathedra.
        /// </summary>
        /// <value>cathedra.</value>
        public string Cathedra { get; set; }

        /// <summary>
        /// Gets or sets the short mark.
        /// </summary>
        /// <value>short mark.</value>
        public int? ShortMark { get; set; }

        /// <summary>
        /// Gets or sets the national mark.
        /// </summary>
        /// <value>national mark.</value>
        public string NationalMark { get; set; }

        /// <summary>
        /// Gets or sets the full mark.
        /// </summary>
        /// <value>Full mark.</value>
        public int? FullMark { get; set; }

        /// <summary>
        /// Gets or sets the ECTS mark.
        /// </summary>
        /// <value>ECTS mark.</value>
        public string ECTSMark { get; set; }

        /// <summary>
        /// Gets or sets the credits.
        /// </summary>
        /// <value>credits.</value>
        public float? Credits { get; set; }

        /// <summary>
        /// Gets or sets the control type.
        /// </summary>
        /// <value>Control type.</value>
        public string Control { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the debt is present.
        /// </summary>
        /// <value>value indicating whether the debt is present.</value>
        public bool IsDebt { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>date.</value>
        public string Date { get; set; }
    }
}
