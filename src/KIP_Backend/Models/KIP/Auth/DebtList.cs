using System.Diagnostics.CodeAnalysis;

namespace KIP_Backend.Models.KIP.Auth
{
    /// <summary>
    /// Debt List KIP.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DebtList
    {
        /// <summary>
        /// Gets or sets the id of subject.
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the prof.
        /// </summary>
        public string Prof { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation of cathedra.
        /// </summary>
        public string ShortCathedra { get; set; }

        /// <summary>
        /// Gets or sets the cathedra.
        /// </summary>
        public string Cathedra { get; set; }

        /// <summary>
        /// Gets or sets the course.
        /// </summary>
        public int Course { get; set; }

        /// <summary>
        /// Gets or sets the Semester.
        /// </summary>
        public int Semester { get; set; }

        /// <summary>
        /// Gets or sets the credits.
        /// </summary>
        public float? Credits { get; set; }

        /// <summary>
        /// Gets or sets the control type.
        /// </summary>
        public string Control { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public string Date { get; set; }
    }
}
