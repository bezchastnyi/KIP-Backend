using System;
using System.ComponentModel.DataAnnotations;

namespace KIP_server_GET.Models
{
    public class StudentSchedule
    {
        [Required]
        public int GroupID { get; set; }
        public Group Group { get; set; }

        [Required]
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }

        public int BuildingID { get; set; }
        public Building Building { get; set; }

        public int AudienceID { get; set; }
        public Audience Audience { get; set; }

        public int StudentScheduleID { get; set; }

        public bool Week { get; set; }

        [Required]
        public DateTime Time { get; set; }
    }
}