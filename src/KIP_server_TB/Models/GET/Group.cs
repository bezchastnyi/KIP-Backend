// <copyright file="Group.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace KIP_server_TB.Models.GET
{
    /// <summary>
    /// Groups KiP.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Gets or sets the id of groups.
        /// </summary>
        /// <value>ID of group.</value>
        public int GroupID { get; set; }

        /// <summary>
        /// Gets or sets the name of groups.
        /// </summary>
        /// <value>Name of group.</value>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the course of groups.
        /// </summary>
        /// <value>Course of group.</value>
        public int Course { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Schedule Is Present for the current group.
        /// </summary>
        /// <value>Course of group.</value>
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
        /// Gets or sets the id of faculties.
        /// </summary>
        /// <value>ID of faculty.</value>
        public int FacultyID { get; set; }

        /// <summary>
        /// Gets or sets the faculties.
        /// </summary>
        /// <value>Faculty.</value>
        public Faculty Faculty { get; set; }
    }
}
