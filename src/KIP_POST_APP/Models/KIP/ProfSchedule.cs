﻿using KIP_POST_APP.Models.KIP.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace KIP_POST_APP.Models.KIP
{
    public class ProfSchedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        [Required(ErrorMessage = "ProfScheduleID is required")]
        public int ProfScheduleID { get; set; }

        [Required(ErrorMessage = "SubjectName is required")]
        [Column(TypeName = "varchar(200)")]
        public string SubjectName { get; set; }

        [Required(ErrorMessage = "Week is required")]
        public Week week { get; set; }

        [Required(ErrorMessage = "day is required")]
        public Day day { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [Column(TypeName = "varchar(100)")]
        public string Type { get; set; }


        [Index]
        [Required(ErrorMessage = "ProfID is required")]
        public int ProfID { get; set; }
        public Prof Prof { get; set; }

        public int? BuildingID { get; set; }
        public Building Building { get; set; }

        public int? AudienceID { get; set; }
        public Audience Audience { get; set; }

        public List<int?> GroupID { get; set; }
        public List<Group> Group { get; set; }
    }
}