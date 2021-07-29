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
        /// NoAuthModeIntent.
        /// </summary>
        public const string GroupScheduleIntentFallback = "Group Schedule Intent - fallback";

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
    }
#pragma warning restore SA1124 // Do not use regions
}
