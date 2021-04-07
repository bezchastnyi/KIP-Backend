using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_POST_APP.Models.KIPDB
{
    public class Cathedra
    {
        [Required]
        public int FacultyID { get; set; }
        public Faculty Faculty { get; set; }

        [Required]
        public int CathedraID { get; set; }

        [Column(TypeName = "varchar(7)")]
        public string CathedraShortName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string CathedraName { get; set; }
    }
}
