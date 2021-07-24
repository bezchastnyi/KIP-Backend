using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KIP_server_GET.Constants;
using KIP_server_GET.Models.KIP;
using KIP_server_GET.Models.KIP.Helpers;
using KIP_server_GET.V1.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace KIP_server_GET.Services
{
    /// <summary>
    /// Db service.
    /// </summary>
    public static class DbService
    {
        /// <summary>
        /// Db service.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The logger.</param>
        /// <returns>Task.</returns>
        public static async Task<(
            List<Faculty> facultyList,
            List<Group> groupList,
            List<Cathedra> cathedraList,
            List<Building> buildingList,
            List<Audience> audienceList,
            List<Prof> profList,
            List<StudentSchedule> studentScheduleList,
            List<StudentSchedule> studentSchedule2List,
            List<ProfSchedule> profScheduleList,
            List<ProfSchedule> profSchedule2List,
            List<AudienceSchedule> AudienceScheduleList,
            List<AudienceSchedule> AudienceSchedule2List)>
            GetDataAsync(ILogger<DbUpdateController> logger, IMapper mapper)
        {
            var kipFacultyList = await MappedDataToKIPDB.GetFacultyListKIPAsync(logger, mapper);

            var kipGroupListByFaculty = await MappedDataToKIPDB.GetGroupListByFacultyKIPAsync(
                kipFacultyList, logger, mapper);

            var kipCathedraListByFaculty = await MappedDataToKIPDB.GetCathedraListByFacultyKIPAsync(
                kipFacultyList, logger, mapper);

            var kipBuildingList = await MappedDataToKIPDB.GetBuildingListKIPAsync(logger, mapper);

            var kipAudienceListByBuilding = await MappedDataToKIPDB.GetAudienceListByBuildingKIPAsync(
                kipBuildingList, logger, mapper);

            var kipProfListByCathedra = await MappedDataToKIPDB.GetProfListByCathedraKIPAsync(
                kipCathedraListByFaculty, logger, mapper);

            DbUpdateController.Week = Week.UnPaired;
            var kipScheduleByProf = await MappedDataToKIPDB.GetScheduleListByProfAsync(
                kipProfListByCathedra, logger, mapper);

            var kipScheduleByGroup = await MappedDataToKIPDB.GetScheduleListByGroupAsync(
                kipGroupListByFaculty, logger, mapper);

            var kipScheduleByAudience = await MappedDataToKIPDB.GetScheduleListByAudienceAsync(
                kipAudienceListByBuilding, logger, mapper);

            DbUpdateController.Week = Week.Paired;
            var kipSchedule2ByProf = await MappedDataToKIPDB.GetSchedule2ListByProfAsync(
                kipProfListByCathedra, logger, mapper);

            var kipSchedule2ByGroup = await MappedDataToKIPDB.GetSchedule2ListByGroupAsync(
                kipGroupListByFaculty, logger, mapper);

            var kipSchedule2ByAudience = await MappedDataToKIPDB.GetSchedule2ListByAudienceAsync(
                kipAudienceListByBuilding, logger, mapper);

            return (kipFacultyList, kipGroupListByFaculty, kipCathedraListByFaculty, kipBuildingList, kipAudienceListByBuilding, kipProfListByCathedra,
                    kipScheduleByGroup, kipSchedule2ByGroup, kipScheduleByProf, kipSchedule2ByProf, kipScheduleByAudience, kipSchedule2ByAudience);
        }

        /// <summary>
        /// Db service.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="config">The logger.</param>
        /// <returns>Task.</returns>
        public static async Task CleanDbAsync(ILogger<DbUpdateController> logger, IConfiguration config)
        {
            var clean = config["DB:Clean"];
            if (string.IsNullOrEmpty(clean))
            {
                // log
                return;
            }

            logger.Log(LogLevel.Information, $"DataBase clean option = {clean}");

            if (clean.ToLower() == "true")
            {
                logger.Log(LogLevel.Information, "Start cleaning DB...");

                var connectionString = config.GetConnectionString("PostgresConnection");
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    if (connection.State.ToString() == "Open")
                    {
                        logger.Log(LogLevel.Information, "Connection opened");

                        using (var command = new NpgsqlCommand(
                            $"TRUNCATE TABLE " +
                            $"\"{CustomNames.Audience}\", " +
                            $"\"{CustomNames.Building}\", " +
                            $"\"{CustomNames.Cathedra}\", " +
                            $"\"{CustomNames.Faculty}\", " +
                            $"\"{CustomNames.Group}\", " +
                            $"\"{CustomNames.Prof}\", " +
                            $"\"{CustomNames.AudienceSchedule}\", " +
                            $"\"{CustomNames.ProfSchedule}\", " +
                            $"\"{CustomNames.StudentSchedule}\";", connection))
                        {
                            logger.Log(LogLevel.Information, $"Executing query: {command.CommandText}");

                            await command.ExecuteNonQueryAsync();
                            logger.Log(LogLevel.Information, "DataBase is cleaned");
                        }
                    }

                    logger.Log(LogLevel.Error, "Unable to open connection to DB");
                }
            }
        }

        /// <summary>
        /// Db service.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="config">The logger.</param>
        /// <param name="tableName">The logger.</param>
        /// <returns>Task.</returns>
        public static async Task CleanTableAsync(ILogger<DbUpdateController> logger, IConfiguration config, string tableName)
        {
            var clean = config["DB:Clean"];
            if (string.IsNullOrEmpty(clean))
            {
                // log
                return;
            }

            logger.Log(LogLevel.Information, $"DataBase clean option = {clean}");

            if (clean.ToLower() == "true")
            {
                logger.Log(LogLevel.Information, "Start cleaning DB...");

                var connectionString = config.GetConnectionString("PostgresConnection");
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    if (connection.State.ToString() == "Open")
                    {
                        logger.Log(LogLevel.Information, "Connection opened");

                        using (var command = new NpgsqlCommand($"TRUNCATE TABLE \"{tableName}\";", connection))
                        {
                            logger.Log(LogLevel.Information, $"Executing query: {command.CommandText}");

                            await command.ExecuteNonQueryAsync();
                            logger.Log(LogLevel.Information, "DataBase is cleaned");
                        }
                    }

                    logger.Log(LogLevel.Error, "Unable to open connection to DB");
                }
            }
        }
    }
}
