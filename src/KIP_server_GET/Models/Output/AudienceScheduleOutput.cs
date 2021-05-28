// <copyright file="AudienceScheduleOutput.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using KIP_POST_APP.Models.KIP.Helpers;

namespace KIP_server_GET.Models.Output
{
    /// <summary>
    /// Audience Schedule Output model.
    /// </summary>
    public class AudienceScheduleOutput
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
        /// Gets or sets names of groups.
        /// </summary>
        public string GroupNames { get; set; }

        /// <summary>
        /// Gets or sets the name of teacher.
        /// </summary>
        public string ProfName { get; set; }
    }
}
