// <copyright file="Group.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_POST_APP.Models.KIP
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
        [Key]
        [Index]
        [Required(ErrorMessage = "GroupID is required")]
        public int GroupID { get; set; }

        /// <summary>
        /// Gets or sets the name of groups.
        /// </summary>
        /// <value>Name of group.</value>
        [Required(ErrorMessage = "GroupName is required")]
        [Column(TypeName = "varchar(100)")]
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the course of groups.
        /// </summary>
        /// <value>Course of group.</value>
        [Required(ErrorMessage = "Course is required")]
        [Range(1, 6, ErrorMessage = "Course must be between 1 and 6")]
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
        [Index]
        [Required(ErrorMessage = "FacultyID is required")]
        public int FacultyID { get; set; }

        /// <summary>
        /// Gets or sets the faculties.
        /// </summary>
        /// <value>Faculty.</value>
        [ForeignKey("FacultyID")]
        public Faculty Faculty { get; set; }
    }
}
