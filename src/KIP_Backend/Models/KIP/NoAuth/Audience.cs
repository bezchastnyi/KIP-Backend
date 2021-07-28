using System.Collections.Generic;

namespace KIP_Backend.Models.KIP.NoAuth
{
    /// <summary>
    /// Audiences KIP.
    /// </summary>
    public class Audience
    {
        /// <summary>
        /// Gets or sets the id of audiences.
        /// </summary>
        public int AudienceId { get; set; }

        /// <summary>
        /// Gets or sets the name of audiences.
        /// </summary>
        public string AudienceName { get; set; }

        /// <summary>
        /// Gets or sets the number of seats in audiences.
        /// </summary>
        public int? NumberOfSeats { get; set; }

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
        /// Gets or sets the id of building.
        /// </summary>
        public int BuildingId { get; set; }
    }
}
