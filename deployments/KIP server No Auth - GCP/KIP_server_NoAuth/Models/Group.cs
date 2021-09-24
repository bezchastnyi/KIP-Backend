using System.Collections.Generic;

namespace KIP_Backend.Models.NoAuth
{
    /// <summary>
    /// Groups KiP.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Gets or sets the id of groups.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the name of groups.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the course of groups.
        /// </summary>
        public int Course { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Schedule Is Present for the current group.
        /// </summary>
        public List<bool> ScheduleIsPresent { get; set; } = new List<bool>()
        {
            false,
            false,
            false,
            false,
            false,
            false,
        };

        /// <summary>
        /// Gets or sets the id of faculties.
        /// </summary>
        public int FacultyId { get; set; }
    }
}
