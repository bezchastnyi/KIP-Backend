using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_server_GET.Models
{
    public class Faculty
    {
        [Required]
        public int FacultyID { get; set; }

        [Column(TypeName = "varchar(7)")]
        public string FacultyShortName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string FacultyName { get; set; }
    }
}