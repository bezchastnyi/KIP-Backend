// <copyright file="GetDataFromKHPIDB.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using KIP_POST_APP.Models.KHPI;
using Newtonsoft.Json;

namespace KIP_POST_APP.Services
{
    /// <summary>
    /// Pulling data from the KhPI database.
    /// </summary>
    public static class GetDataFromKHPIDB
    {
        private const string FacultyList = @"http://schedule.kpi.kharkov.ua/json/FacultyList";
        private const string GroupListByFacultyId = @"http://schedule.kpi.kharkov.ua/json/GroupByFacultyList/";
        private const string ScheduleByGroupId = @"http://schedule.kpi.kharkov.ua/json/Schedule/";
        private const string Schedule2ByGroupId = @"http://schedule.kpi.kharkov.ua/json/Schedule2/";
        private const string CathedrasListByFacultyId = @"http://schedule.kpi.kharkov.ua/JSON/DeptsByFacultyP/";
        private const string ProfListByCathedraId = @"http://schedule.kpi.kharkov.ua/JSON/PrepodListByDeptP/";
        private const string ProfScheduleByProfId = @"http://schedule.kpi.kharkov.ua/JSON/ScheduleP/";
        private const string ProfSchedule2ByProfId = @"http://schedule.kpi.kharkov.ua/JSON/Schedule2P/";
        private const string BuildingList = @"http://schedule.kpi.kharkov.ua/JSON/BuildingList";
        private const string AuditoryListByBuildingId = @"http://schedule.kpi.kharkov.ua/JSON/AudListByBuilding/";
        private const string ScheduleByAuditoryId = @"http://schedule.kpi.kharkov.ua/JSON/ScheduleA/";
        private const string Schedule2ByAuditoryId = @"http://schedule.kpi.kharkov.ua/JSON/Schedule2A/";
        private const string AuditoryListByCathedraId = @"http://schedule.kpi.kharkov.ua/JSON/AudListByKafedra/";

        // http://schedule.kpi.kharkov.ua/json/SearchGroups/поисковыйзапрос/
        // http://schedule.kpi.kharkov.ua/json/SearchPrepod/поисковыйзапрос/

        /// <summary>
        /// Getting a list of faculties using asynchrony.
        /// </summary>
        /// <returns>
        /// List of faculties.
        /// </returns>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static async Task<IEnumerable<FacultyKHPI>> GetFacultyListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetJsonListDataAsync<FacultyKHPI>(FacultyList, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting a list of groups by faculty ID using asynchrony.
        /// </summary>
        /// <returns>
        /// List of list of groups by faculty.
        /// </returns>
        /// <param name="facultyId">Faculty ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static async Task<IEnumerable<GroupKHPI>> GetGroupListByFacultyIdAsync(int facultyId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetJsonListDataAsync<GroupKHPI>(GroupListByFacultyId + facultyId, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting a list of departments by faculty ID using asynchrony.
        /// </summary>
        /// <returns>
        /// List of list of departments by faculty.
        /// </returns>
        /// <param name="facultyId">Faculty ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static async Task<IEnumerable<CathedraKHPI>> GetCathedraListByFacultyIdAsync(int facultyId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetJsonListDataAsync<CathedraKHPI>(CathedrasListByFacultyId + facultyId, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting a group schedule for an unpaired week using asynchrony.
        /// </summary>
        /// <returns>
        /// Schedule of groups for an unpaired week.
        /// </returns>
        /// <param name="groupId">Group ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static async Task<ScheduleByGroupKHPI> GetScheduleByGroupIdAsync(int groupId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetJsonDataAsync<ScheduleByGroupKHPI>(ScheduleByGroupId + groupId, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting a group schedule for a paired week using asynchrony.
        /// </summary>
        /// <returns>
        /// Schedule of groups for a paired week.
        /// </returns>
        /// <param name="groupId">Group ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static async Task<ScheduleByGroupKHPI> GetSchedule2ByGroupIdAsync(int groupId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetJsonDataAsync<ScheduleByGroupKHPI>(Schedule2ByGroupId + groupId, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting a teachers schedulefor an unpaired week using asynchrony.
        /// </summary>
        /// <returns>
        /// Schedule of teachers for an unpaired week.
        /// </returns>
        /// <param name="profId">Teacher ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static async Task<ScheduleByProfKHPI> GetScheduleByProfIdAsync(int profId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetJsonDataAsync<ScheduleByProfKHPI>(ProfScheduleByProfId + profId, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting a teachers schedule for a paired week using asynchrony.
        /// </summary>
        /// <returns>
        /// Schedule of teachers for a paired week.
        /// </returns>
        /// <param name="profId">Teacher ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static async Task<ScheduleByProfKHPI> GetSchedule2ByProfIdAsync(int profId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetJsonDataAsync<ScheduleByProfKHPI>(ProfSchedule2ByProfId + profId, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting the list of buildings using asynchrony.
        /// </summary>
        /// <returns>
        /// List of buildings.
        /// </returns>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static async Task<IEnumerable<BuildingKHPI>> GetBuildingListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetJsonListDataAsync<BuildingKHPI>(BuildingList, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting a list of audiences in the building using asynchrony.
        /// </summary>
        /// <returns>
        /// List of audiences in the building.
        /// </returns>
        /// <param name="buildingId">Building Id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static async Task<IEnumerable<AudienceKHPI>> GetAudienceListByBuildingIdAsync(int buildingId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetJsonListDataAsync<AudienceKHPI>(AuditoryListByBuildingId + buildingId, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting a list of teachers of the department using asynchrony.
        /// </summary>
        /// <returns>
        /// List of teachers of the department.
        /// </returns>
        /// <param name="cathedraId">Cathedra Id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static async Task<IEnumerable<ProfKHPI>> GetProfListByCathedraIdAsync(int cathedraId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetJsonListDataAsync<ProfKHPI>(ProfListByCathedraId + cathedraId, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting list data from json using asynchrony.
        /// </summary>
        /// <returns>
        /// List data from json.
        /// </returns>
        /// <param name="url">Link to json.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private static async Task<IEnumerable<T>> GetJsonListDataAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            using (var web = new WebClient())
            {
                var jsonData = string.Empty;

                try
                {
                    jsonData = await web.DownloadStringTaskAsync(url);
                    if (jsonData.Contains("<!DOCTYPE html>"))
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + ": " + e.StackTrace);
                }

                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonData);
            }
        }

        /// <summary>
        /// Getting data from json using asynchrony.
        /// </summary>
        /// <returns>
        /// Data from json.
        /// </returns>
        /// <param name="url">Link to json.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private static async Task<T> GetJsonDataAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            using (var web = new WebClient())
            {
                var jsonData = string.Empty;

                try
                {
                    jsonData = await web.DownloadStringTaskAsync(url);
                    if (jsonData.Contains("<!DOCTYPE html>"))
                    {
                        return default;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + ": " + e.StackTrace + "\n URL = " + url);
                }

                return JsonConvert.DeserializeObject<T>(jsonData);
            }
        }
    }
}
