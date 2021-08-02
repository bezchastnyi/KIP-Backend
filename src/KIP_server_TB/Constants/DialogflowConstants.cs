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
        public const string ProfScheduleIntentProfFallback = "Prof Schedule Intent - Prof - fallback";

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
        public const string AudienceScheduleIntentAudienceFallback = "Audience Schedule Intent - Audience - fallback";

        #endregion
    }
#pragma warning restore SA1124 // Do not use regions
}
