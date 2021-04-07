using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_server_GET.Models.KIP
{
    public class Cathedra
    {
        [Key]
        [Index]
        [Required(ErrorMessage = "CathedraID is required")]
        public int CathedraID { get; set; }

        [Required(ErrorMessage = "CathedraName is required")]
        [Column(TypeName = "varchar(100)")]
        public string CathedraName { get; set; }

        [Column(TypeName = "varchar(7)")]
        public string CathedraShortName { get; set; }


        [Required(ErrorMessage = "FacultyID is required")]
        public int FacultyID { get; set; }
        [ForeignKey("FacultyID")]
        public Faculty Faculty { get; set; }
    }
}