
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_server_GET.Models
{
    public class Student
    {
        [Required]
        public int GroupID { get; set; }
        public Group Group { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string StudentName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string StudentSurname { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string StudentPatronymic { get; set; }

        public DateTime StudentBDay { get; set; }
    }
}