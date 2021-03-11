using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_server_GET.Models
{
    [Keyless]
    public class Auth
    {
        [Required]
        public int StudentID { get; set; }
        public Student Student { get; set; }

        public int AuthID { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Password { get; set; }
    }
}