// <copyright file="DebtList.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_server_AUTH.Models.KIP
{
    /// <summary>
    /// Debt List KIP.
    /// </summary>
    public class DebtList
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
        /// Gets or sets the course.
        /// </summary>
        /// <value>course.</value>
        public int Course { get; set; }

        /// <summary>
        /// Gets or sets the Semester.
        /// </summary>
        /// <value>Semester.</value>
        public int Semester { get; set; }

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
        /// Gets or sets the date.
        /// </summary>
        /// <value>date.</value>
        public string Date { get; set; }
    }
}
