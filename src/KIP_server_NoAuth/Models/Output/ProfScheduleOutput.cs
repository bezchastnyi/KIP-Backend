﻿using KIP_Backend.Models.Helpers;

namespace KIP_server_NoAuth.Models.Output
{
    /// <summary>
    /// Audience Schedule Output model.
    /// </summary>
    public class ProfScheduleOutput
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
        /// Gets or sets the name of audience.
        /// </summary>
        public string AudienceName { get; set; }

        /// <summary>
        /// Gets or sets names of groups.
        /// </summary>
        public string GroupNames { get; set; }
    }
}
