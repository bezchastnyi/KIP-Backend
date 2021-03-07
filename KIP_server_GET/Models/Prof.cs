
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_server_GET.Models
{
    public class Prof
    {
        [Required]
        public int CathedraID { get; set; }
        public Cathedra Cathedra { get; set; }

        [Required]
        public int ProfID { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string ProfName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string ProfSurname { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string ProfPatronymic { get; set; }
    }
}