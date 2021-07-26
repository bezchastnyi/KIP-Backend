// <copyright file="ProfSchedule.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;
using KIP_Backend.Models.KIP.Helpers;

namespace KIP_Backend.Models.KIP
{
    /// <summary>
    /// Building of the schedule of teachers model.
    /// </summary>
    public class ProfSchedule
    {
        /// <summary>
        /// Gets or sets the id of schedule of teachers.
        /// </summary>
        public int ProfScheduleID { get; set; }

        /// <summary>
        /// Gets or sets the name of subject.
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Gets or sets the week.
        /// </summary>
        public Week Week { get; set; }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        public Day Day { get; set; }

        /// <summary>
        /// Gets or sets the type of para.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the number of para.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the id of teacher.
        /// </summary>
        public int ProfID { get; set; }

        /// <summary>
        /// Gets or sets the id of building.
        /// </summary>
        public int? BuildingID { get; set; }

        /// <summary>
        /// Gets or sets the id of audience.
        /// </summary>
        public int? AudienceID { get; set; }

        /// <summary>
        /// Gets or sets the name of audience.
        /// </summary>
        public string AudienceName { get; set; }

        /// <summary>
        /// Gets or sets the id of group.
        /// </summary>
        public List<int?> GroupID { get; set; }

        /// <summary>
        /// Gets or sets names of groups.
        /// </summary>
        public string GroupNames { get; set; }
    }
}
