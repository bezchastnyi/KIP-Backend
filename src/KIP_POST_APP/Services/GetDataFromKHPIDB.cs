using KIP_POST_APP.Models.KHPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace KIP_POST_APP.Services
{
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

        //http://schedule.kpi.kharkov.ua/json/SearchGroups/поисковыйзапрос/
        //http://schedule.kpi.kharkov.ua/json/SearchPrepod/поисковыйзапрос/

        [Obsolete]
        public static async Task<IEnumerable<Faculty_KHPI>> GetFacultyListAsync(CancellationToken stoppingToken = default)
        {
            try 
            { 
                return await GetJsonListDataAsync<Faculty_KHPI>(FacultyList, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<IEnumerable<Group_KHPI>> GetGroupListByFacultyIdAsync(int FacultyId, CancellationToken stoppingToken = default)
        {
            try 
            { 
                return await GetJsonListDataAsync<Group_KHPI>(GroupListByFacultyId + FacultyId, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<IEnumerable<Cathedra_KHPI>> GetCathedraListByFacultyIdAsync(int FacultyId, CancellationToken stoppingToken = default)
        {
            try 
            { 
                return await GetJsonListDataAsync<Cathedra_KHPI>(CathedrasListByFacultyId + FacultyId, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<ScheduleByGroup_KHPI> GetScheduleByGroupIdAsync(int GroupId, CancellationToken stoppingToken = default)
        {
            try 
            {
                return await GetJsonDataAsync<ScheduleByGroup_KHPI>(ScheduleByGroupId + GroupId, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<ScheduleByGroup_KHPI> GetSchedule2ByGroupIdAsync(int GroupId, CancellationToken stoppingToken = default)
        {
            try
            {
                return await GetJsonDataAsync<ScheduleByGroup_KHPI>(Schedule2ByGroupId + GroupId, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<ScheduleByProf_KHPI> GetScheduleByProfIdAsync(int ProfId, CancellationToken stoppingToken = default)
        {
            try
            {
                return await GetJsonDataAsync<ScheduleByProf_KHPI>(ProfScheduleByProfId + ProfId, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<ScheduleByProf_KHPI> GetSchedule2ByProfIdAsync(int ProfId, CancellationToken stoppingToken = default)
        {
            try
            {
                return await GetJsonDataAsync<ScheduleByProf_KHPI>(ProfSchedule2ByProfId + ProfId, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<IEnumerable<Building_KHPI>> GetBuildingListAsync(CancellationToken stoppingToken = default)
        {
            try 
            { 
                return await GetJsonListDataAsync<Building_KHPI>(BuildingList, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<IEnumerable<Audience_KHPI>> GetAudienceListByBuildingIdAsync(int BuildingId, CancellationToken stoppingToken = default)
        {
            try
            { 
                return await GetJsonListDataAsync<Audience_KHPI>(AuditoryListByBuildingId + BuildingId, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<IEnumerable<Prof_KHPI>> GetProfListByCathedraIdAsync(int CathedraId, CancellationToken stoppingToken = default)
        {
            try
            {
                return await GetJsonListDataAsync<Prof_KHPI>(ProfListByCathedraId + CathedraId, stoppingToken);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        private static async Task<IEnumerable<T>> GetJsonListDataAsync<T>(string url, CancellationToken stoppingToken = default)
        {
            using (var web = new WebClient())
            {
                var JsonData = string.Empty;

                try
                {
                    JsonData = web.DownloadString(url);
                    if (JsonData.Contains("<!DOCTYPE html>"))
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + ": " + e.StackTrace);
                }

                return JsonConvert.DeserializeObject<IEnumerable<T>>(JsonData);
            }
        }

        [Obsolete]
        private static async Task<T> GetJsonDataAsync<T>(string url, CancellationToken stoppingToken = default)
        {
            using (var web = new WebClient())
            {
                var JsonData = string.Empty;

                try
                {
                    JsonData = web.DownloadString(url);
                    if (JsonData.Contains("<!DOCTYPE html>"))
                    {
                        return default;
                    }
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message + ": " + e.StackTrace + "\n URL = " + url);
                }

                return JsonConvert.DeserializeObject<T>(JsonData);
            }
        }
    }
}
