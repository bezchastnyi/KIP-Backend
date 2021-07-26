// <copyright file="Prof.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace KIP_Backend.Models.KIP
{
    /// <summary>
    /// Teachers KIP.
    /// </summary>
    public class Prof
    {
        /// <summary>
        /// Gets or sets the id of teachers.
        /// </summary>
        public int ProfID { get; set; }

        /// <summary>
        /// Gets or sets the surname of teacher.
        /// </summary>
        public string ProfSurname { get; set; }

        /// <summary>
        /// Gets or sets the name of teacher.
        /// </summary>
        public string ProfName { get; set; }

        /// <summary>
        /// Gets or sets the patronymic of teacher.
        /// </summary>
        public string ProfPatronymic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Schedule Is Present for the current prof.
        /// </summary>
        public List<bool> ScheduleIsPresent { get; set; } = new List<bool>()
        {
            false,
            false,
            false,
            false,
            false,
            false,
        };

        /// <summary>
        /// Gets or sets the id of departments.
        /// </summary>
        public int CathedraID { get; set; }
    }
}
