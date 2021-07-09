// <copyright file="StudentSchedule.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using KIP_server_TB.Models.GET.Helpers;

namespace KIP_server_TB.Models.GET
{
    /// <summary>
    /// Building of the schedule of groups model.
    /// </summary>
    public class StudentSchedule
    {
        /// <summary>
        /// Gets or sets the schedule or groups.
        /// </summary>
        /// <value>Schedule of groups.</value>
        public int StudentScheduleID { get; set; }

        /// <summary>
        /// Gets or sets the name of subject.
        /// </summary>
        /// <value>Name of subject.</value>
        public string SubjectName { get; set; }

        /// <summary>
        /// Gets or sets the week.
        /// </summary>
        /// <value>Week.</value>
        public Week Week { get; set; }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        /// <value>Day.</value>
        public Day Day { get; set; }

        /// <summary>
        /// Gets or sets the type of para.
        /// </summary>
        /// <value>Type of para.</value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the number of para.
        /// </summary>
        /// <value>Number of para.</value>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the id of group.
        /// </summary>
        /// <value>ID of group.</value>
        public int GroupID { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>Group.</value>
        public Group Group { get; set; }

        /// <summary>
        /// Gets or sets the id of building.
        /// </summary>
        /// <value>ID of building.</value>
        public int? BuildingID { get; set; }

        /// <summary>
        /// Gets or sets the building.
        /// </summary>
        /// <value>Building.</value>
        public Building Building { get; set; }

        /// <summary>
        /// Gets or sets the id of audience.
        /// </summary>
        /// <value>ID of audience.</value>
        public int? AudienceID { get; set; }

        /// <summary>
        /// Gets or sets the name of audience.
        /// </summary>
        /// <value>ID of teacher.</value>
        public string AudienceName { get; set; }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>Audience.</value>
        public Audience Audience { get; set; }

        /// <summary>
        /// Gets or sets the id of teacher.
        /// </summary>
        /// <value>ID of teacher.</value>
        public int? ProfID { get; set; }

        /// <summary>
        /// Gets or sets the name of teacher.
        /// </summary>
        /// <value>ID of teacher.</value>
        public string ProfName { get; set; }

        /// <summary>
        /// Gets or sets the teacher.
        /// </summary>
        /// <value>Teacher.</value>
        public Prof Prof { get; set; }
    }
}
