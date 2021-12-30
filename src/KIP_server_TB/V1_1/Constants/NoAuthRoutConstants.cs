namespace KIP_server_TB.V1_1.Constants
{
    /// <summary>
    /// NoAuth Rout Constants.
    /// </summary>
    public static class NoAuthRoutConstants
    {
        /// <summary>
        /// AllBuildings.
        /// </summary>
        public const string AllBuildings = "v1/Building";

        /// <summary>
        /// AudienceById.
        /// </summary>
        public const string AudienceById = "v1/Audience/{0}";

        /// <summary>
        /// AudiencesByBuildingId.
        /// </summary>
        public const string AudiencesByBuildingId = "v1/Audience/Building/{0}";

        /// <summary>
        /// AudienceScheduleByAudienceId.
        /// </summary>
        public const string AudienceScheduleByAudienceId = "v1/AudienceSchedule/Audience/{0}";

        /// <summary>
        /// AudienceScheduleByAudienceIdAndDay.
        /// </summary>
        public const string AudienceScheduleByAudienceIdAndDay = "v1/AudienceSchedule/Audience/{0}/Day/{1}";

        /// <summary>
        /// AllFaculties.
        /// </summary>
        public const string AllFaculties = "v1/Faculty";

        /// <summary>
        /// FacultyById.
        /// </summary>
        public const string FacultyById = "v1/Faculty/{0}";

        /// <summary>
        /// GroupsByFacultyId.
        /// </summary>
        public const string GroupsByFacultyId = "v1/Group/Faculty/{0}";

        /// <summary>
        /// StudentScheduleByGroupId.
        /// </summary>
        public const string StudentScheduleByGroupId = "v1/StudentSchedule/Group/{0}";

        /// <summary>
        /// StudentScheduleByGroupIdAndDay.
        /// </summary>
        public const string StudentScheduleByGroupIdAndDay = "v1/StudentSchedule/Group/{0}/Day/{1}";

        /// <summary>
        /// CathedrasByFacultyId.
        /// </summary>
        public const string CathedrasByFacultyId = "v1/Cathedra/Faculty/{0}";

        /// <summary>
        /// ProfById.
        /// </summary>
        public const string ProfById = "v1/Prof/{0}";

        /// <summary>
        /// ProfsByCathedraId.
        /// </summary>
        public const string ProfsByCathedraId = "v1/Prof/Cathedra/{0}";

        /// <summary>
        /// ProfScheduleByProfId.
        /// </summary>
        public const string ProfScheduleByProfId = "v1/ProfSchedule/Prof/{0}";

        /// <summary>
        /// ProfScheduleByProfIdAndDay.
        /// </summary>
        public const string ProfScheduleByProfIdAndDay = "v1/ProfSchedule/Prof/{0}/Day/{1}";
    }
}
