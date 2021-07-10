using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace KIP_server_AUTH.Constants
{
    /// <summary>
    /// Event identifiers.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "Logging")]
    public static class EventIds
    {
        #region Debug
        #endregion

        #region Information

        /// <summary>
        /// Identifier of success data getting.
        /// </summary>
        public static readonly EventId DataGetSuccessEventId = new EventId(2200, "DATA_GET_SUCCESSFUL");

        #endregion

        #region Warning
        #endregion

        #region Error

        /// <summary>
        /// Retrieving data from KhPI db error identifier.
        /// </summary>
        public static readonly EventId RetrieveDataFromKhPIDbErrorEventId = new EventId(2400, "RETRIEVE_DATA_FROM_KHPI_DB_ERROR");

        /// <summary>
        /// Mapping data error identifier.
        /// </summary>
        public static readonly EventId MapDataErrorEventId = new EventId(2401, "MAP_DATA_ERROR");

        /// <summary>
        /// Getting data error identifier.
        /// </summary>
        public static readonly EventId GetDataUnexpectedErrorEventId = new EventId(2402, "GET_DATA_UNEXPECTED_ERROR");

        #endregion
    }
}
