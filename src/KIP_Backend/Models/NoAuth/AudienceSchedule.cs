using System.Collections.Generic;
using KIP_Backend.Models.Helpers;

namespace KIP_Backend.Models.NoAuth
{
    /// <summary>
    /// Building of the schedule of audience model.
    /// </summary>
    public class AudienceSchedule
    {
        /// <summary>
        /// Gets or sets the schedule of audiences.
        /// </summary>
        public int AudienceScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the name of subject.
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Gets or sets the week.
        /// </summary>
        public Week Week { get; set; }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        public Day Day { get; set; }

        /// <summary>
        /// Gets or sets the type of para.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the number of para.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the id of building.
        /// </summary>
        public int BuildingId { get; set; }

        /// <summary>
        /// Gets or sets the id of audience.
        /// </summary>
        public int AudienceId { get; set; }

        /// <summary>
        /// Gets or sets the id of group.
        /// </summary>
        public List<int?> GroupId { get; set; }

        /// <summary>
        /// Gets or sets names of groups.
        /// </summary>
        public string GroupNames { get; set; }

        /// <summary>
        /// Gets or sets the id of teacher.
        /// </summary>
        public int? ProfId { get; set; }

        /// <summary>
        /// Gets or sets the name of teacher.
        /// </summary>
        public string ProfName { get; set; }
    }
}
