namespace KIP_Backend.Models.NoAuth
{
    /// <summary>
    /// Buildings KIP.
    /// </summary>
    public class Building
    {
        /// <summary>
        /// Gets or sets the id of buildings.
        /// </summary>
        public int BuildingId { get; set; }

        /// <summary>
        /// Gets or sets the full name of buildings.
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// Gets or sets the short name of buildings.
        /// </summary>
        public string BuildingShortName { get; set; }

        /// <summary>
        /// Gets or sets the number of audiences in building.
        /// </summary>
        public int? NumberOfAudiences { get; set; }
    }
}
