﻿using KIP_Backend.Models.Helpers;

namespace KIP_server_NoAuth.Models.Output
{
    /// <summary>
    /// Student Schedule Output model.
    /// </summary>
    public class StudentScheduleOutput
    {
        /// <summary>
        /// Gets or sets the name of subject.
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Gets or sets the type of para.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the number of para.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the week.
        /// </summary>
        public Week Week { get; set; }

        /// <summary>
        /// Gets or sets name of audience.
        /// </summary>
        public string AudienceName { get; set; }

        /// <summary>
        /// Gets or sets the name of teacher.
        /// </summary>
        public string ProfName { get; set; }
    }
}
