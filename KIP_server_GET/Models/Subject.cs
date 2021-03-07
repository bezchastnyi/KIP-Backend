
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_server_GET.Models
{
    public class Subject
    {
        [Required]
        public int CathedraID { get; set; }
        public Cathedra Cathedra { get; set; }

        [Required]
        public int SubjectID { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string SubjectName { get; set; }
    }
}