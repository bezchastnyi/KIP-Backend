using System.Collections.Generic;
using System.Threading.Tasks;
using KIP_Backend.Extensions;
using KIP_server_NoAuth.Models.KhPI;
using KIP_server_NoAuth.V1.Controllers;
using Microsoft.Extensions.Logging;

namespace KIP_server_NoAuth.Services
{
    /// <summary>
    /// Pulling data from the KhPI database.
    /// </summary>
    public static class GetService
    {
        private const string FacultyListUrl = @"http://schedule.kpi.kharkov.ua/json/FacultyList";
        private const string GroupListByFacultyUrl = @"http://schedule.kpi.kharkov.ua/json/GroupByFacultyList/";
        private const string ScheduleByGroupUrl = @"http://schedule.kpi.kharkov.ua/json/Schedule/";
        private const string Schedule2ByGroupUrl = @"http://schedule.kpi.kharkov.ua/json/Schedule2/";
        private const string CathedrasListByFacultyUrl = @"http://schedule.kpi.kharkov.ua/JSON/DeptsByFacultyP/";
        private const string ProfListByCathedraUrl = @"http://schedule.kpi.kharkov.ua/JSON/PrepodListByDeptP/";
        private const string ScheduleByProfUrl = @"http://schedule.kpi.kharkov.ua/JSON/ScheduleP/";
        private const string Schedule2ByProfUrl = @"http://schedule.kpi.kharkov.ua/JSON/Schedule2P/";
        private const string BuildingListUrl = @"http://schedule.kpi.kharkov.ua/JSON/BuildingList";
        private const string AudienceListByBuildingUrl = @"http://schedule.kpi.kharkov.ua/JSON/AudListByBuilding/";
        private const string ScheduleByAudienceUrl = @"http://schedule.kpi.kharkov.ua/JSON/ScheduleA/";
        private const string Schedule2ByAudienceUrl = @"http://schedule.kpi.kharkov.ua/JSON/Schedule2A/";

        /// <summary>
        /// Getting a list of faculties using asynchronous.
        /// </summary>
        /// <returns>List of faculties.</returns>
        /// <param name="logger">Group ID.</param>
        public static async Task<IEnumerable<FacultyKhPI>> GetFacultiesAsync(ILogger logger)
        {
            return await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<FacultyKhPI>(FacultyListUrl, logger);
        }

        /// <summary>
        /// Getting a list of groups by faculty ID using asynchronous.
        /// </summary>
        /// <returns>List of list of groups by faculty.</returns>
        /// <param name="facultyId">Faculty ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<IEnumerable<GroupKhPI>> GetGroupsAsync(int facultyId, ILogger logger)
        {
            return await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<GroupKhPI>(GroupListByFacultyUrl + facultyId, logger);
        }

        /// <summary>
        /// Getting a list of departments by faculty ID using asynchronous.
        /// </summary>
        /// <returns>List of list of departments by faculty.</returns>
        /// <param name="facultyId">Faculty ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<IEnumerable<CathedraKhPI>> GetCathedrasAsync(int facultyId, ILogger logger)
        {
            return await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<CathedraKhPI>(CathedrasListByFacultyUrl + facultyId, logger);
        }

        /// <summary>
        /// Getting a group schedule for an unpaired week using asynchronous.
        /// </summary>
        /// <returns>Schedule of groups for an unpaired week.</returns>
        /// <param name="groupId">Group ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<ScheduleKhPI> GetScheduleByGroupAsync(int groupId, ILogger logger)
        {
            return await ConvertExtensions.ConvertJsonDataToModelAsync<ScheduleKhPI>(ScheduleByGroupUrl + groupId, logger);
        }

        /// <summary>
        /// Getting a group schedule for a paired week using asynchronous.
        /// </summary>
        /// <returns>Schedule of groups for a paired week.</returns>
        /// <param name="groupId">Group ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<ScheduleKhPI> GetSchedule2ByGroupAsync(int groupId, ILogger logger)
        {
            return await ConvertExtensions.ConvertJsonDataToModelAsync<ScheduleKhPI>(Schedule2ByGroupUrl + groupId, logger);
        }

        /// <summary>
        /// Getting a teachers schedule for an unpaired week using asynchronous.
        /// </summary>
        /// <returns>Schedule of teachers for an unpaired week.</returns>
        /// <param name="profId">Teacher ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<ScheduleKhPI> GetScheduleByProfAsync(int profId, ILogger logger)
        {
            return await ConvertExtensions.ConvertJsonDataToModelAsync<ScheduleKhPI>(ScheduleByProfUrl + profId, logger);
        }

        /// <summary>
        /// Getting a teachers schedule for a paired week using asynchronous.
        /// </summary>
        /// <returns>Schedule of teachers for a paired week.</returns>
        /// <param name="profId">Teacher ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<ScheduleKhPI> GetSchedule2ByProfAsync(int profId, ILogger logger)
        {
            return await ConvertExtensions.ConvertJsonDataToModelAsync<ScheduleKhPI>(Schedule2ByProfUrl + profId, logger);
        }

        /// <summary>
        /// Getting an audience schedule for an unpaired week using asynchronous.
        /// </summary>
        /// <returns>Schedule of teachers for an unpaired week.</returns>
        /// <param name="audienceId">Audience ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<ScheduleKhPI> GetScheduleByAudienceAsync(int audienceId, ILogger logger)
        {
            return await ConvertExtensions.ConvertJsonDataToModelAsync<ScheduleKhPI>(ScheduleByAudienceUrl + audienceId, logger);
        }

        /// <summary>
        /// Getting an audience schedule for an paired week using asynchronous.
        /// </summary>
        /// <returns>Schedule of teachers for an unpaired week.</returns>
        /// <param name="audienceId">Audience ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<ScheduleKhPI> GetSchedule2ByAudienceAsync(int audienceId, ILogger logger)
        {
            return await ConvertExtensions.ConvertJsonDataToModelAsync<ScheduleKhPI>(Schedule2ByAudienceUrl + audienceId, logger);
        }

        /// <summary>
        /// Getting the list of buildings using asynchronous.
        /// </summary>
        /// <returns>List of buildings.</returns>
        /// <param name="logger">Group ID.</param>
        public static async Task<IEnumerable<BuildingKhPI>> GetBuildingsAsync(ILogger logger)
        {
            return await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<BuildingKhPI>(BuildingListUrl, logger);
        }

        /// <summary>
        /// Getting a list of audiences in the building using asynchronous.
        /// </summary>
        /// <returns>List of audiences in the building.</returns>
        /// <param name="buildingId">Building Id.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<IEnumerable<AudienceKhPI>> GetAudiencesAsync(int buildingId, ILogger logger)
        {
            return await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<AudienceKhPI>(AudienceListByBuildingUrl + buildingId, logger);
        }

        /// <summary>
        /// Getting a list of teachers of the department using asynchronous.
        /// </summary>
        /// <returns>List of teachers of the department.</returns>
        /// <param name="cathedraId">Cathedra Id.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<IEnumerable<ProfKhPI>> GetProfsAsync(int cathedraId, ILogger logger)
        {
            return await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<ProfKhPI>(ProfListByCathedraUrl + cathedraId, logger);
        }
    }
}
