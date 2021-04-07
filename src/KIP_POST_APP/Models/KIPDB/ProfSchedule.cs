using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KIP_POST_APP.Models.KIPDB
{
    public class ProfSchedule
    {
        [Required]
        public int ProfID { get; set; }
        public Prof Prof { get; set; }

        public int BuildingID { get; set; }
        public Building Building { get; set; }

        public int AudienceID { get; set; }
        public Audience Audience { get; set; }

        public List<int> GroupId { get; set; }
        public List<Group> Group { get; set; }

        public int ProfScheduleID { get; set; }

        public string SubjectName { get; set; }

        public Week week { get; set; }

        [Required]
        public Day day { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
