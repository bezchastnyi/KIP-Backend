using KIP_server_GET.Models.KIP.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_server_GET.Models.KIP
{
    public class StudentSchedule
    {
        [Key]
        [Index]
        [Required(ErrorMessage = "StudentScheduleID is required")]
        public int StudentScheduleID { get; set; }

        [Required(ErrorMessage = "SubjectName is required")]
        [Column(TypeName = "varchar(200)")]
        public string SubjectName { get; set; }

        [Required(ErrorMessage = "Week is required")]
        public Week Week { get; set; }

        [Required(ErrorMessage = "day is required")]
        public Day day { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; }


        [Required(ErrorMessage = "GroupID is required")]
        public int GroupID { get; set; }
        [ForeignKey("GroupID")]
        public Group Group { get; set; }

        [Required(ErrorMessage = "BuildingID is required")]
        public int BuildingID { get; set; }
        [ForeignKey("BuildingID")]
        public Building Building { get; set; }

        [Required(ErrorMessage = "AudienceID is required")]
        public int AudienceID { get; set; }
        [ForeignKey("AudienceID")]
        public Audience Audience { get; set; }

        [Required(ErrorMessage = "ProfID is required")]
        public int ProfID { get; set; }
        [ForeignKey("ProfID")]
        public Prof Prof { get; set; }
    }
}