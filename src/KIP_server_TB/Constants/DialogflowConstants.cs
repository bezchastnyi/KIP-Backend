using System.Diagnostics.CodeAnalysis;

namespace KIP_server_TB.Constants
{
    /// <summary>
    /// DialogflowConstants.
    /// </summary>
#pragma warning disable SA1124 // Do not use regions
    [ExcludeFromCodeCoverage]
    public static class DialogflowConstants
    {
        /// <summary>
        /// ExitIntent.
        /// </summary>
        public const string ExitIntent = "Exit Intent";

        #region No auth mode

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string NoAuthModeIntent = "No Auth Mode Intent";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string FacultyIntent = "Faculty Intent";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string CourseIntent = "Course Intent";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string CourseIntentFallback = "Course Intent - fallback";

        #endregion

        #region Schedule

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string ScheduleIntent = "ScheduleIntent";

        /// <summary>
        /// GroupScheduleIntent.
        /// </summary>
        public const string GroupScheduleIntent = "Group Schedule Intent";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string GroupScheduleIntentDay = "Group Schedule Intent - Day";

        #endregion

        #region Prof schedule

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string ProfScheduleIntent = "Prof Schedule Intent";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string ProfScheduleIntentCathedra = "Prof Schedule Intent - Cathedra";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string ProfScheduleIntentProf = "Prof Schedule Intent - Prof";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string ProfScheduleIntentDay = "Prof Schedule Intent - Day";

        #endregion

        #region Audience schedule

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string AudienceScheduleIntent = "Audience Schedule Intent";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string AudienceScheduleIntentBuilding = "Audience Schedule Intent - Building";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string AudienceScheduleIntentAudience = "Audience Schedule Intent - Audience";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string AudienceScheduleIntentDay = "Audience Schedule Intent - Day";

        #endregion

        #region Auth mode

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string EmailIntent = "Email Intent";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string PasswordIntent = "Password Intent";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string StudentInformationIntent = "Student Information Intent";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string StudentDebtsIntent = "Student Debts Intent";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string CurrentRankIntent = "Current Rank Intent";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string StudyingPlanIntent = "Studying Plan Intent - Semester";

        /// <summary>
        /// NoAuthModeIntent.
        /// </summary>
        public const string SemesterMarksIntent = "Semester Marks Intent - Semester";

        #endregion
    }
#pragma warning restore SA1124 // Do not use regions
}
