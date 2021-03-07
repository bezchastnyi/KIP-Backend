
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_server_GET.Models
{
    public class News
    {
        public int FacultyID { get; set; }
        public Faculty Faculty { get; set; }

        [Required]
        public int NewsID { get; set; }

        [Required]
        [Column(TypeName = "varchar(1000)")]
        public string NewsText { get; set; }
    }
}