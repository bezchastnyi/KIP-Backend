﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIP_server_GET.Models
{
    public class Building
    {
        [Required]
        public int BuildingID { get; set; }

        [Column(TypeName = "varchar(5)")]
        public string BuildingShortName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string BuildingName { get; set; }
    }
}