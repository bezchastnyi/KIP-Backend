using System.Diagnostics.CodeAnalysis;

namespace KIP_Backend.Models.KIP.Auth
{
    /// <summary>
    /// Semester Marks List KIP.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SemesterMarksList
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
        /// Gets or sets the short mark.
        /// </summary>
        public int? ShortMark { get; set; }

        /// <summary>
        /// Gets or sets the national mark.
        /// </summary>
        public string NationalMark { get; set; }

        /// <summary>
        /// Gets or sets the full mark.
        /// </summary>
        public int? FullMark { get; set; }

        /// <summary>
        /// Gets or sets the ECTS mark.
        /// </summary>
        public string ECTSMark { get; set; }

        /// <summary>
        /// Gets or sets the credits.
        /// </summary>
        public float? Credits { get; set; }

        /// <summary>
        /// Gets or sets the control type.
        /// </summary>
        public string Control { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the debt is present.
        /// </summary>
        public bool IsDebt { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public string Date { get; set; }
    }
}
