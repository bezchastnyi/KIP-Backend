using System;
using System.ComponentModel.DataAnnotations;

namespace KIP_server_GET.Models
{
    public class ProfSchedule
    {
        public Prof Prof { get; set; }

        public Subject Subject { get; set; }

        public Building Building { get; set; }

        public Audience Audience { get; set; }

        public int ProfScheduleID { get; set; }

        public bool Week { get; set; }

        [Required]
        public DateTime Time { get; set; }
    }
}