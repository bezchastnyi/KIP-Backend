using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_POST_APP.Models.KIPDB
{
    public class Audience
    {
        [Required]
        public int BuildingID { get; set; }
        public Building Building { get; set; }

        [Required]
        public int AudienceID { get; set; }

        [Required]
        [Column(TypeName = "varchar(7)")]
        public string AudienceName { get; set; }

        public int NumberOfSeats { get; set; }
    }
}
