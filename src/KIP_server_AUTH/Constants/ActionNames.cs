using System.Diagnostics.CodeAnalysis;

namespace KIP_server_Auth.Constants
{
    /// <summary>
    /// Action name constants.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ActionNames
    {
        /// <summary>
        /// Get CurrentRank.
        /// </summary>
        public const string GetCurrentRank = "GetCurrentRank";

        /// <summary>
        /// Get DebtList.
        /// </summary>
        public const string GetDebtList = "GetDebtList";

        /// <summary>
        /// Get PersonalInformation.
        /// </summary>
        public const string GetPersonalInformation = "GetPersonalInformation";

        /// <summary>
        /// Get SemesterMarksList.
        /// </summary>
        public const string GetSemesterMarksList = "GetSemesterMarksList";

        /// <summary>
        /// Get SemesterStudyingPlan.
        /// </summary>
        public const string GetSemesterStudyingPlan = "GetSemesterStudyingPlan";

        /// <summary>
        /// Retrieve data from KhPI Db.
        /// </summary>
        public const string RetrieveDataFromKhPIDb = "RetrieveDataFromKhPIDb";

        /// <summary>
        /// Map data.
        /// </summary>
        public const string MapData = "MapData";
    }
}
