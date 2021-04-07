using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_POST_APP.Models.KIPDB
{
    public class Group
    {
        [Required]
        public int FacultyID { get; set; }
        public Faculty Faculty { get; set; }

        [Required]
        public int GroupID { get; set; }

        [Required]
        public int Course { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string GroupName { get; set; }
    }
}