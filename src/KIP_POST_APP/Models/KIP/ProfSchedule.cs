using KIP_POST_APP.Models.KIP.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_POST_APP.Models.KIP
{
    public class ProfSchedule
    {
        [Key]
        [Index]
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


        [Required(ErrorMessage = "ProfID is required")]
        public int ProfID { get; set; }
        [ForeignKey("ProfID")]
        public Prof Prof { get; set; }

        [Required(ErrorMessage = "BuildingID is required")]
        public int BuildingID { get; set; }
        [ForeignKey("BuildingID")]
        public Building Building { get; set; }

        [Required(ErrorMessage = "AudienceID is required")]
        public int AudienceID { get; set; }
        [ForeignKey("AudienceID")]
        public Audience Audience { get; set; }

        [Required(ErrorMessage = "GroupID is required")]
        public List<int> GroupID { get; set; }
        public List<Group> Group { get; set; }
    }
}