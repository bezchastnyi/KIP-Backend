// <copyright file="GetDataFromKHPIDB.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using KIP_server_GET.Models.KHPI;
using Newtonsoft.Json;

namespace KIP_server_GET.Services
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

        /// <summary>
        /// Getting a list of faculties using asynchrony.
        /// </summary>
        /// <returns>
        /// List of faculties.
        /// </returns>
        public static async Task<IEnumerable<FacultyKHPI>> GetFacultyListAsync()
        {
            try
            {
                return await GetJsonListDataAsync<FacultyKHPI>(FacultyList);
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
        public static async Task<IEnumerable<GroupKHPI>> GetGroupListByFacultyIdAsync(int facultyId)
        {
            try
            {
                return await GetJsonListDataAsync<GroupKHPI>(GroupListByFacultyId + facultyId);
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
        public static async Task<IEnumerable<CathedraKHPI>> GetCathedraListByFacultyIdAsync(int facultyId)
        {
            try
            {
                return await GetJsonListDataAsync<CathedraKHPI>(CathedrasListByFacultyId + facultyId);
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
        public static async Task<ScheduleByGroupKHPI> GetScheduleByGroupIdAsync(int groupId)
        {
            try
            {
                return await GetJsonDataAsync<ScheduleByGroupKHPI>(ScheduleByGroupId + groupId);
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
        public static async Task<ScheduleByGroupKHPI> GetSchedule2ByGroupIdAsync(int groupId)
        {
            try
            {
                return await GetJsonDataAsync<ScheduleByGroupKHPI>(Schedule2ByGroupId + groupId);
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
        public static async Task<ScheduleByProfKHPI> GetScheduleByProfIdAsync(int profId)
        {
            try
            {
                return await GetJsonDataAsync<ScheduleByProfKHPI>(ProfScheduleByProfId + profId);
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
        public static async Task<ScheduleByProfKHPI> GetSchedule2ByProfIdAsync(int profId)
        {
            try
            {
                return await GetJsonDataAsync<ScheduleByProfKHPI>(ProfSchedule2ByProfId + profId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting an audience schedule for an unpaired week using asynchrony.
        /// </summary>
        /// <returns>
        /// Schedule of teachers for an unpaired week.
        /// </returns>
        /// <param name="audienceId">Audience ID.</param>
        public static async Task<ScheduleByAudienceKHPI> GetScheduleByAudienceIdAsync(int audienceId)
        {
            try
            {
                return await GetJsonDataAsync<ScheduleByAudienceKHPI>(ScheduleByAuditoryId + audienceId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting an audience schedule for an paired week using asynchrony.
        /// </summary>
        /// <returns>
        /// Schedule of teachers for an unpaired week.
        /// </returns>
        /// <param name="audienceId">Audience ID.</param>
        public static async Task<ScheduleByAudienceKHPI> GetSchedule2ByAudienceIdAsync(int audienceId)
        {
            try
            {
                return await GetJsonDataAsync<ScheduleByAudienceKHPI>(Schedule2ByAuditoryId + audienceId);
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
        public static async Task<IEnumerable<BuildingKHPI>> GetBuildingListAsync()
        {
            try
            {
                return await GetJsonListDataAsync<BuildingKHPI>(BuildingList);
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
        public static async Task<IEnumerable<AudienceKHPI>> GetAudienceListByBuildingIdAsync(int buildingId)
        {
            try
            {
                return await GetJsonListDataAsync<AudienceKHPI>(AuditoryListByBuildingId + buildingId);
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
        public static async Task<IEnumerable<ProfKHPI>> GetProfListByCathedraIdAsync(int cathedraId)
        {
            try
            {
                return await GetJsonListDataAsync<ProfKHPI>(ProfListByCathedraId + cathedraId);
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
        private static async Task<IEnumerable<T>> GetJsonListDataAsync<T>(string url)
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
        private static async Task<T> GetJsonDataAsync<T>(string url)
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
