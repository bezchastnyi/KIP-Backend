using System.Diagnostics.CodeAnalysis;

namespace KIP_server_TB.Constants
{
    /// <summary>
    /// DialogflowConstants.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "Dialogflow constants")]
    public static class DialogflowConstants
    {
        #region Basic

        /// <summary>
        /// Exit intent.
        /// </summary>
        public const string ExitIntent = "Exit Intent";

        #endregion

        #region No auth mode

        /// <summary>
        /// No auth mode intent.
        /// </summary>
        public const string NoAuthModeIntent = "No Auth Mode Intent";

        /// <summary>
        /// Faculty intent.
        /// </summary>
        public const string FacultyIntent = "Faculty Intent";

        /// <summary>
        /// Course intent.
        /// </summary>
        public const string CourseIntent = "Course Intent";

        /// <summary>
        /// Course intent - fallback.
        /// </summary>
        public const string CourseIntentFallback = "Course Intent - fallback";

        #endregion

        #region Schedule

        /// <summary>
        /// ScheduleIntent.
        /// </summary>
        public const string ScheduleIntent = "ScheduleIntent";

        /// <summary>
        /// Group Schedule Intent.
        /// </summary>
        public const string GroupScheduleIntent = "Group Schedule Intent";

        /// <summary>
        /// Group Schedule Intent - Day.
        /// </summary>
        public const string GroupScheduleIntentDay = "Group Schedule Intent - Day";

        /// <summary>
        /// Prof Schedule Intent.
        /// </summary>
        public const string ProfScheduleIntent = "Prof Schedule Intent";

        /// <summary>
        /// Prof Schedule Intent - Cathedra.
        /// </summary>
        public const string ProfScheduleIntentCathedra = "Prof Schedule Intent - Cathedra";

        /// <summary>
        /// Prof Schedule Intent - Prof.
        /// </summary>
        public const string ProfScheduleIntentProf = "Prof Schedule Intent - Prof";

        /// <summary>
        /// Prof Schedule Intent - Day.
        /// </summary>
        public const string ProfScheduleIntentDay = "Prof Schedule Intent - Day";

        /// <summary>
        /// Audience Schedule Intent.
        /// </summary>
        public const string AudienceScheduleIntent = "Audience Schedule Intent";

        /// <summary>
        /// Audience Schedule Intent - Building.
        /// </summary>
        public const string AudienceScheduleIntentBuilding = "Audience Schedule Intent - Building";

        /// <summary>
        /// Audience Schedule Intent - Audience.
        /// </summary>
        public const string AudienceScheduleIntentAudience = "Audience Schedule Intent - Audience";

        /// <summary>
        /// Audience Schedule Intent - Day.
        /// </summary>
        public const string AudienceScheduleIntentDay = "Audience Schedule Intent - Day";

        #endregion

        #region Auth mode

        /// <summary>
        /// Email Intent.
        /// </summary>
        public const string EmailIntent = "Email Intent";

        /// <summary>
        /// Password Intent.
        /// </summary>
        public const string PasswordIntent = "Password Intent";

        /// <summary>
        /// Student Information Intent.
        /// </summary>
        public const string StudentInformationIntent = "Student Information Intent";

        /// <summary>
        /// Student Debts Intent.
        /// </summary>
        public const string StudentDebtsIntent = "Student Debts Intent";

        /// <summary>
        /// Current Rank Intent.
        /// </summary>
        public const string CurrentRankIntent = "Current Rank Intent";

        /// <summary>
        /// Studying Plan Intent - Semester.
        /// </summary>
        public const string StudyingPlanIntent = "Studying Plan Intent - Semester";

        /// <summary>
        /// Semester Marks Intent - Semester.
        /// </summary>
        public const string SemesterMarksIntent = "Semester Marks Intent - Semester";

        #endregion
    }
}
