using System.Diagnostics.CodeAnalysis;

namespace KIP_Backend.Models.Auth
{
    /// <summary>
    /// Personal Information KIP.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PersonalInformation
    {
        /// <summary>
        /// Gets or sets the id of student.
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Gets or sets the FirstName of student.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName of student.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the Patronymic of student.
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Gets or sets the Course of student.
        /// </summary>
        public int Course { get; set; }

        /// <summary>
        /// Gets or sets the GroupId of student.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the Group of student.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the FacultyId of student.
        /// </summary>
        public int FacultyId { get; set; }

        /// <summary>
        /// Gets or sets the Faculty of student.
        /// </summary>
        public string Faculty { get; set; }

        /// <summary>
        /// Gets or sets the CathedraId of student.
        /// </summary>
        public int CathedraId { get; set; }

        /// <summary>
        /// Gets or sets the Cathedra of student.
        /// </summary>
        public string Cathedra { get; set; }

        /// <summary>
        /// Gets or sets Specialization of student.
        /// </summary>
        public string Specialization { get; set; }

        /// <summary>
        /// Gets or sets Specialty of student.
        /// </summary>
        public string Specialty { get; set; }

        /// <summary>
        /// Gets or sets StudyingProgram of student.
        /// </summary>
        public string StudyingProgram { get; set; }

        /// <summary>
        /// Gets or sets StudyingLevel of student.
        /// </summary>
        public string StudyingLevel { get; set; }

        /// <summary>
        /// Gets or sets StudyingForm of student.
        /// </summary>
        public string StudyingForm { get; set; }

        /// <summary>
        /// Gets or sets BudgetForm of student.
        /// </summary>
        public string BudgetForm { get; set; }
    }
}
