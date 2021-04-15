// <copyright file="ProfSchedule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KIP_POST_APP.Models.KIP.Helpers;

namespace KIP_POST_APP.Models.KIP
{
    /// <summary>
    /// Building of the schedule of teachers model.
    /// </summary>
    public class ProfSchedule
    {
        /// <summary>
        /// Gets or sets the id of schedule of teachers.
        /// </summary>
        /// <value>ID of schedule of teachers.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage = "ProfScheduleID is required")]
        public int ProfScheduleID { get; set; }

        /// <summary>
        /// Gets or sets the name of subject.
        /// </summary>
        /// <value>Name of subject.</value>
        [Required(ErrorMessage = "SubjectName is required")]
        [Column(TypeName = "varchar(200)")]
        public string SubjectName { get; set; }

        /// <summary>
        /// Gets or sets the week.
        /// </summary>
        /// <value>Week.</value>
        [Required(ErrorMessage = "Week is required")]
        public Week Week { get; set; }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        /// <value>Day.</value>
        [Required(ErrorMessage = "day is required")]
        public Day Day { get; set; }

        /// <summary>
        /// Gets or sets the type of para.
        /// </summary>
        /// <value>Type of para.</value>
        [Required(ErrorMessage = "Type is required")]
        [Column(TypeName = "varchar(100)")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the id of teacher.
        /// </summary>
        /// <value>ID of teacher.</value>
        [Index]
        [Required(ErrorMessage = "ProfID is required")]
        public int ProfID { get; set; }

        /// <summary>
        /// Gets or sets the teacher.
        /// </summary>
        /// <value>Teacher.</value>
        public Prof Prof { get; set; }

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
        /// Gets or sets the audience.
        /// </summary>
        /// <value>Audience.</value>
        public Audience Audience { get; set; }

        /// <summary>
        /// Gets or sets the id of group.
        /// </summary>
        /// <value>ID of group.</value>
        public List<int?> GroupID { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>Group.</value>
        public List<Group> Group { get; set; }
    }
}
