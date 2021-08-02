namespace KIP_Backend.Models.NoAuth
{
    /// <summary>
    /// Faculties KIP.
    /// </summary>
    public class Faculty
    {
        /// <summary>
        /// Gets or sets the id of faculties.
        /// </summary>
        public int FacultyId { get; set; }

        /// <summary>
        /// Gets or sets the full name of faculties.
        /// </summary>
        public string FacultyName { get; set; }

        /// <summary>
        /// Gets or sets the short name of faculties.
        /// </summary>
        public string FacultyShortName { get; set; }
    }
}
