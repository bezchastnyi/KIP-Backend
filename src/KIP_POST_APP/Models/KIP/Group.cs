using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_POST_APP.Models.KIP
{
    public class Group
    {
        [Key]
        [Index]
        [Required(ErrorMessage = "GroupID is required")]
        public int GroupID { get; set; }

        [Required(ErrorMessage = "GroupName is required")]
        [Column(TypeName = "varchar(100)")]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "Course is required")]
        [Range(1, 6, ErrorMessage = "Course must be between 1 and 6")]
        public int Course { get; set; }


        [Required(ErrorMessage = "FacultyID is required")]
        public int FacultyID { get; set; }
        [ForeignKey("FacultyID")]
        public Faculty Faculty { get; set; }

        [Required(ErrorMessage = "Cathedra is required")]
        public int CathedraID { get; set; }
        [ForeignKey("CathedraID")]
        public Cathedra Cathedra { get; set; }
    }
}