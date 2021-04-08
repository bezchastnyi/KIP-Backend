using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_POST_APP.Models.KIP
{
    public class Prof
    {
        [Key]
        [Index]
        [Required(ErrorMessage = "ProfID is required")]
        public int ProfID { get; set; }

        [Required(ErrorMessage = "ProfSurname is required")]
        [Column(TypeName = "varchar(100)")]
        public string ProfSurname { get; set; }

        [Required(ErrorMessage = "ProfName is required")]
        [Column(TypeName = "varchar(100)")]
        public string ProfName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string ProfPatronymic { get; set; }


        [Required(ErrorMessage = "CathedraID is required")]
        public int CathedraID { get; set; }
        [ForeignKey("CathedraID")]
        public Cathedra Cathedra { get; set; }
    }
}