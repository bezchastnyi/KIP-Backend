using System;
using System.ComponentModel.DataAnnotations;

namespace KIP_server_GET.Models
{
    public class ProfSchedule
    {
        [Required]
        public int ProfID { get; set; }
        public Prof Prof { get; set; }

        [Required]
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }

        public int BuildingID { get; set; }
        public Building Building { get; set; }

        public int AudienceID { get; set; }
        public Audience Audience { get; set; }

        public int ProfScheduleID { get; set; }

        public bool Week { get; set; }

        [Required]
        public DateTime Time { get; set; }
    }
}