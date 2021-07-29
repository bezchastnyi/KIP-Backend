// <copyright file="Prof.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_POST_APP.Models.KIP
{
    /// <summary>
    /// Teachers KIP.
    /// </summary>
    public class Prof
    {
        /// <summary>
        /// Gets or sets the id of teachers.
        /// </summary>
        /// <value>ID of teacher.</value>
        [Key]
        [Index]
        [Required(ErrorMessage = "ProfID is required")]
        public int ProfID { get; set; }

        /// <summary>
        /// Gets or sets the surname of teacher.
        /// </summary>
        /// <value>Surname of teacher.</value>
        [Required(ErrorMessage = "ProfSurname is required")]
        [Column(TypeName = "varchar(100)")]
        public string ProfSurname { get; set; }

        /// <summary>
        /// Gets or sets the name of teacher.
        /// </summary>
        /// <value>Name of teacher.</value>
        [Required(ErrorMessage = "ProfName is required")]
        [Column(TypeName = "varchar(100)")]
        public string ProfName { get; set; }

        /// <summary>
        /// Gets or sets the patronymic of teacher.
        /// </summary>
        /// <value>Patronymic of teacher.</value>
        [Column(TypeName = "varchar(100)")]
        public string ProfPatronymic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Schedule Is Present for the current prof.
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
        /// Gets or sets the id of departments.
        /// </summary>
        /// <value>ID of departments.</value>
        [Required(ErrorMessage = "CathedraID is required")]
        public int CathedraID { get; set; }

        /// <summary>
        /// Gets or sets the departments.
        /// </summary>
        /// <value>Departments.</value>
        [ForeignKey("CathedraID")]
        public Cathedra Cathedra { get; set; }

        /*
        /// <summary>
        /// Gets or sets the id of departments.
        /// </summary>
        /// <value>ID of departments.</value>
        [Required(ErrorMessage = "CathedraID is required")]
        public List<int> CathedraID { get; set; }

        /// <summary>
        /// Gets or sets the departments.
        /// </summary>
        /// <value>Departments.</value>
        public List<Cathedra> Cathedra { get; set; }
        */
    }
}
