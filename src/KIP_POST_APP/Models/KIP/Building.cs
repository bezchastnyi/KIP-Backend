using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_POST_APP.Models.KIP
{
    public class Building
    {
        [Key]
        [Index]
        [Required(ErrorMessage = "BuildingID is required")]
        public int BuildingID { get; set; }

        [Required(ErrorMessage = "BuildingName is required")]
        [Column(TypeName = "varchar(100)")]
        public string BuildingName { get; set; }

        [Column(TypeName = "varchar(5)")]
        public string BuildingShortName { get; set; }
    }
}