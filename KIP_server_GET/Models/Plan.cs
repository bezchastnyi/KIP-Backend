
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_server_GET.Models
{
    public class Plan
    {
        [Required]
        public int StudentID { get; set; }
        public Student Student { get; set; }

        [Required]
        public int PlanID { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string PlanText { get; set; }
}
}