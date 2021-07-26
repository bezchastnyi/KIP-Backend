// <copyright file="ReceiveService.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using KIP_server_NoAuth.Models.KhPI;
using KIP_server_NoAuth.V1.Controllers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace KIP_server_NoAuth.Services
{
    /// <summary>
    /// Pulling data from the KhPI database.
    /// </summary>
    public static class ReceiveService
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
        /// Getting a list of faculties using asynchrony.
        /// </summary>
        /// <returns>List of faculties.</returns>
        /// <param name="logger">Group ID.</param>
        public static async Task<IEnumerable<FacultyKhPI>> GetFacultiesAsync(ILogger<DbUpdateController> logger)
        {
            return await ConvertJsonDataToListOfModelsAsync<FacultyKhPI>(FacultyListUrl, logger);
        }

        /// <summary>
        /// Getting a list of groups by faculty ID using asynchrony.
        /// </summary>
        /// <returns>List of list of groups by faculty.</returns>
        /// <param name="facultyId">Faculty ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<IEnumerable<GroupKHPI>> GetGroupsAsync(int facultyId, ILogger<DbUpdateController> logger)
        {
            return await ConvertJsonDataToListOfModelsAsync<GroupKHPI>(GroupListByFacultyUrl + facultyId, logger);
        }

        /// <summary>
        /// Getting a list of departments by faculty ID using asynchrony.
        /// </summary>
        /// <returns>List of list of departments by faculty.</returns>
        /// <param name="facultyId">Faculty ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<IEnumerable<CathedraKHPI>> GetCathedrasAsync(int facultyId, ILogger<DbUpdateController> logger)
        {
            return await ConvertJsonDataToListOfModelsAsync<CathedraKHPI>(CathedrasListByFacultyUrl + facultyId, logger);
        }

        /// <summary>
        /// Getting a group schedule for an unpaired week using asynchrony.
        /// </summary>
        /// <returns>Schedule of groups for an unpaired week.</returns>
        /// <param name="groupId">Group ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<ScheduleByGroupKHPI> GetScheduleByGroupAsync(int groupId, ILogger<DbUpdateController> logger)
        {
            return await ConvertJsonDataToModelAsync<ScheduleByGroupKHPI>(ScheduleByGroupUrl + groupId, logger);
        }

        /// <summary>
        /// Getting a group schedule for a paired week using asynchrony.
        /// </summary>
        /// <returns>Schedule of groups for a paired week.</returns>
        /// <param name="groupId">Group ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<ScheduleByGroupKHPI> GetSchedule2ByGroupAsync(int groupId, ILogger<DbUpdateController> logger)
        {
            return await ConvertJsonDataToModelAsync<ScheduleByGroupKHPI>(Schedule2ByGroupUrl + groupId, logger);
        }

        /// <summary>
        /// Getting a teachers schedulefor an unpaired week using asynchrony.
        /// </summary>
        /// <returns>Schedule of teachers for an unpaired week.</returns>
        /// <param name="profId">Teacher ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<ScheduleByProfKHPI> GetScheduleByProfAsync(int profId, ILogger<DbUpdateController> logger)
        {
            return await ConvertJsonDataToModelAsync<ScheduleByProfKHPI>(ScheduleByProfUrl + profId, logger);
        }

        /// <summary>
        /// Getting a teachers schedule for a paired week using asynchrony.
        /// </summary>
        /// <returns>Schedule of teachers for a paired week.</returns>
        /// <param name="profId">Teacher ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<ScheduleByProfKHPI> GetSchedule2ByProfAsync(int profId, ILogger<DbUpdateController> logger)
        {
            return await ConvertJsonDataToModelAsync<ScheduleByProfKHPI>(Schedule2ByProfUrl + profId, logger);
        }

        /// <summary>
        /// Getting an audience schedule for an unpaired week using asynchrony.
        /// </summary>
        /// <returns>Schedule of teachers for an unpaired week.</returns>
        /// <param name="audienceId">Audience ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<ScheduleByAudienceKHPI> GetScheduleByAudienceAsync(int audienceId, ILogger<DbUpdateController> logger)
        {
            return await ConvertJsonDataToModelAsync<ScheduleByAudienceKHPI>(ScheduleByAudienceUrl + audienceId, logger);
        }

        /// <summary>
        /// Getting an audience schedule for an paired week using asynchrony.
        /// </summary>
        /// <returns>Schedule of teachers for an unpaired week.</returns>
        /// <param name="audienceId">Audience ID.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<ScheduleByAudienceKHPI> GetSchedule2ByAudienceAsync(int audienceId, ILogger<DbUpdateController> logger)
        {
            return await ConvertJsonDataToModelAsync<ScheduleByAudienceKHPI>(Schedule2ByAudienceUrl + audienceId, logger);
        }

        /// <summary>
        /// Getting the list of buildings using asynchrony.
        /// </summary>
        /// <returns>List of buildings.</returns>
        /// <param name="logger">Group ID.</param>
        public static async Task<IEnumerable<BuildingKHPI>> GetBuildingsAsync(ILogger<DbUpdateController> logger)
        {
            return await ConvertJsonDataToListOfModelsAsync<BuildingKHPI>(BuildingListUrl, logger);
        }

        /// <summary>
        /// Getting a list of audiences in the building using asynchrony.
        /// </summary>
        /// <returns>List of audiences in the building.</returns>
        /// <param name="buildingId">Building Id.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<IEnumerable<AudienceKHPI>> GetAudiencesAsync(int buildingId, ILogger<DbUpdateController> logger)
        {
            return await ConvertJsonDataToListOfModelsAsync<AudienceKHPI>(AudienceListByBuildingUrl + buildingId, logger);
        }

        /// <summary>
        /// Getting a list of teachers of the department using asynchrony.
        /// </summary>
        /// <returns>List of teachers of the department.</returns>
        /// <param name="cathedraId">Cathedra Id.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<IEnumerable<ProfKHPI>> GetProfsAsync(int cathedraId, ILogger<DbUpdateController> logger)
        {
            return await ConvertJsonDataToListOfModelsAsync<ProfKHPI>(ProfListByCathedraUrl + cathedraId, logger);
        }

        /// <summary>
        /// Getting list data from json using asynchrony.
        /// </summary>
        /// <returns>List data from json.</returns>
        /// <param name="url">Link to json.</param>
        /// <param name="logger">Link to json.</param>
        private static async Task<IEnumerable<T>> ConvertJsonDataToListOfModelsAsync<T>(string url, ILogger<DbUpdateController> logger)
        {
            using (var web = new WebClient())
            {
                try
                {
                    var jsonData = await web.DownloadStringTaskAsync(url);
                    if (jsonData.Contains("<!DOCTYPE html>"))
                    {
                        return null;
                    }

                    return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonData);
                }
                catch (Exception e)
                {
                    logger.LogError($"'ConvertJsonDataToModelAsync': Url: {url} Message: {e.Message} StackTrace: {e.StackTrace}");
                    return default;
                }
            }
        }

        /// <summary>
        /// Getting data from json using asynchrony.
        /// </summary>
        /// <returns>Data from json.</returns>
        /// <param name="url">Link to json.</param>
        /// <param name="logger">Link to json.</param>
        private static async Task<T> ConvertJsonDataToModelAsync<T>(string url, ILogger<DbUpdateController> logger)
        {
            using (var web = new WebClient())
            {
                try
                {
                    var jsonData = await web.DownloadStringTaskAsync(url);
                    if (jsonData.Contains("<!DOCTYPE html>"))
                    {
                        return default;
                    }

                    return JsonConvert.DeserializeObject<T>(jsonData);
                }
                catch (Exception e)
                {
                    logger.LogError($"'ConvertJsonDataToModelAsync': Url: {url} Message: {e.Message} StackTrace: {e.StackTrace}");
                    return default;
                }
            }
        }
    }
}
