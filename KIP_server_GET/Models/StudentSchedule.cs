using System;
using System.ComponentModel.DataAnnotations;

namespace KIP_server_GET.Models
{
    public class StudentSchedule
    {
        public Group Group { get; set; }

        public Subject Subject { get; set; }

        public Building Building { get; set; }

        public Audience Audience { get; set; }

        public int StudentScheduleID { get; set; }

        public bool Week { get; set; }

        [Required]
        public DateTime Time { get; set; }
    }
}