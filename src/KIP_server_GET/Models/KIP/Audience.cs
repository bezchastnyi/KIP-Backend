using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_server_GET.Models.KIP
{
    public class Audience
    {
        [Key]
        [Index]
        [Required(ErrorMessage = "AudienceID is required")]
        public int AudienceID { get; set; }

        [Required(ErrorMessage = "AudienceName is required")]
        [Column(TypeName = "varchar(7)")]
        public string AudienceName { get; set; }

        [Range(0, 1000, ErrorMessage = "NumberOfSeats must be between 0 and 1000")]
        public int? NumberOfSeats { get; set; }


        [Required(ErrorMessage = "BuildingID is required")]
        public int BuildingID { get; set; }
        [ForeignKey("BuildingID")]
        public Building Building { get; set; }
    }
}