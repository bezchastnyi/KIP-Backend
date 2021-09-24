using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KIP_Backend.Extensions;
using KIP_Backend.Models.Helpers;
using KIP_server_NoAuth.Models.Helpers;
using KIP_server_NoAuth.Models.KhPI;
using Microsoft.Extensions.Logging;

namespace KIP_server_NoAuth.Services
{
    /// <summary>
    /// Pulling data from the KhPI database.
    /// </summary>
    public static class GetService
    {
        private const string FacultyListUrl = @"https://schedule.kpi.kharkov.ua/json/FacultyList";
        private const string GroupListByFacultyUrl = @"https://schedule.kpi.kharkov.ua/json/GroupByFacultyList/";
        private const string ScheduleByGroupUrl = @"https://schedule.kpi.kharkov.ua/json/Schedule/";
        private const string Schedule2ByGroupUrl = @"https://schedule.kpi.kharkov.ua/json/Schedule2/";
        private const string CathedrasListByFacultyUrl = @"https://schedule.kpi.kharkov.ua/JSON/DeptsByFacultyP/";
        private const string ProfListByCathedraUrl = @"https://schedule.kpi.kharkov.ua/JSON/PrepodListByDeptP/";
        private const string ScheduleByProfUrl = @"https://schedule.kpi.kharkov.ua/JSON/ScheduleP/";
        private const string Schedule2ByProfUrl = @"https://schedule.kpi.kharkov.ua/JSON/Schedule2P/";
        private const string BuildingListUrl = @"https://schedule.kpi.kharkov.ua/JSON/BuildingList";
        private const string AudienceListByBuildingUrl = @"https://schedule.kpi.kharkov.ua/JSON/AudListByBuilding/";
        private const string ScheduleByAudienceUrl = @"https://schedule.kpi.kharkov.ua/JSON/ScheduleA/";
        private const string Schedule2ByAudienceUrl = @"https://schedule.kpi.kharkov.ua/JSON/Schedule2A/";

        /// <summary>
        /// Getting a list of teachers of the department using asynchronous.
        /// </summary>
        /// <returns>List of teachers of the department.</returns>
        /// <typeparam name="T">Data type.</typeparam>
        /// <param name="logger">Group ID.</param>
        /// <param name="id">The id of general entity.</param>
        public static async Task<IEnumerable<T>> GetCollectionOfDataAsync<T>(ILogger logger, int? id = null)
        {
            if (typeof(T) == typeof(FacultyKhPI))
            {
                return await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<T>(FacultyListUrl, logger);
            }

            if (typeof(T) == typeof(GroupKhPI))
            {
                return await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<T>(GroupListByFacultyUrl + id, logger);
            }

            if (typeof(T) == typeof(CathedraKhPI))
            {
                return await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<T>(CathedrasListByFacultyUrl + id, logger);
            }

            if (typeof(T) == typeof(BuildingKhPI))
            {
                return await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<T>(BuildingListUrl, logger);
            }

            if (typeof(T) == typeof(ProfKhPI))
            {
                return await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<T>(ProfListByCathedraUrl + id, logger);
            }

            if (typeof(T) == typeof(AudienceKhPI))
            {
                return await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<T>(AudienceListByBuildingUrl + id, logger);
            }

            throw new ArgumentException($"Action: 'Get collection of data from KhPI' typeparam is not valid ({typeof(T)})");
        }

        /// <summary>
        /// Getting a list of teachers of the department using asynchronous.
        /// </summary>
        /// <returns>List of teachers of the department.</returns>
        /// <param name="id">The id of general entity.</param>
        /// <param name="type">The type.</param>
        /// <param name="week">The week.</param>
        /// <param name="logger">Group ID.</param>
        public static async Task<ScheduleKhPI> GetScheduleAsync(int id, ScheduleType type, Week week, ILogger logger)
        {
            string url;
            switch (type)
            {
                case ScheduleType.GroupSchedule:
                {
                    if (week == Week.UnPaired)
                    {
                        url = ScheduleByGroupUrl;
                        break;
                    }

                    url = Schedule2ByGroupUrl;
                    break;
                }

                case ScheduleType.ProfSchedule:
                {
                    if (week == Week.UnPaired)
                    {
                        url = ScheduleByProfUrl;
                        break;
                    }

                    url = Schedule2ByProfUrl;
                    break;
                }

                case ScheduleType.AudienceSchedule:
                {
                    if (week == Week.UnPaired)
                    {
                        url = ScheduleByAudienceUrl;
                        break;
                    }

                    url = Schedule2ByAudienceUrl;
                    break;
                }

                default:
                {
                    throw new ArgumentException($"Action: 'Get schedule from KhPI' typeparam is not valid ({type})");
                }
            }

            return await ConvertExtensions.ConvertJsonDataToModelAsync<ScheduleKhPI>(url + id, logger);
        }
    }
}
