// <copyright file="PersonalInformation.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_auth_mode.Models.KIP
{
    /// <summary>
    /// Personal Information KIP.
    /// </summary>
    public class PersonalInformation
    {
        /// <summary>
        /// Gets or sets the id of student.
        /// </summary>
        /// <value>ID of student.</value>
        public int StudentId { get; set; }

        /// <summary>
        /// Gets or sets the FirstName of student.
        /// </summary>
        /// <value>FirstName of student.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName of student.
        /// </summary>
        /// <value>LastName of student.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the Patronymic of student.
        /// </summary>
        /// <value>Patronymic of student.</value>
        public string Patronymic { get; set; }

        /// <summary>
        /// Gets or sets the Course of student.
        /// </summary>
        /// <value>Course of student.</value>
        public int Course { get; set; }

        /// <summary>
        /// Gets or sets the GroupId of student.
        /// </summary>
        /// <value>GroupId of student.</value>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the Group of student.
        /// </summary>
        /// <value>Group of student.</value>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the FacultyId of student.
        /// </summary>
        /// <value>FacultyId of student.</value>
        public int FacultyId { get; set; }

        /// <summary>
        /// Gets or sets the Faculty of student.
        /// </summary>
        /// <value>Faculty of student.</value>
        public string Faculty { get; set; }

        /// <summary>
        /// Gets or sets the CathedraId of student.
        /// </summary>
        /// <value>CathedraId of student.</value>
        public int CathedraId { get; set; }

        /// <summary>
        /// Gets or sets the Cathedra of student.
        /// </summary>
        /// <value>Cathedra of student.</value>
        public string Cathedra { get; set; }

        /// <summary>
        /// Gets or sets Specialization of student.
        /// </summary>
        /// <value>Specialization of student.</value>
        public string Specialization { get; set; }

        /// <summary>
        /// Gets or sets Specialty of student.
        /// </summary>
        /// <value>Specialty of student.</value>
        public string Specialty { get; set; }

        /// <summary>
        /// Gets or sets StudyingProgram of student.
        /// </summary>
        /// <value>StudyingProgram of student.</value>
        public string StudyingProgram { get; set; }

        /// <summary>
        /// Gets or sets StudyingLevel of student.
        /// </summary>
        /// <value>StudyingLevel of student.</value>
        public string StudyingLevel { get; set; }

        /// <summary>
        /// Gets or sets StudyingForm of student.
        /// </summary>
        /// <value>StudyingForm of student.</value>
        public string StudyingForm { get; set; }

        /// <summary>
        /// Gets or sets BudgetForm of student.
        /// </summary>
        /// <value>BudgetForm of student.</value>
        public string BudgetForm { get; set; }
    }
}
