using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_POST_APP.Models.KIP
{
    public class Faculty
    {
        [Key]
        [Index]
        [Required(ErrorMessage = "FacultyID is required")]
        public int FacultyID { get; set; }

        [Required(ErrorMessage = "FacultyName is required")]
        [Column(TypeName = "varchar(100)")]
        public string FacultyName { get; set; }

        [Column(TypeName = "varchar(7)")]
        public string FacultyShortName { get; set; }
    }
}