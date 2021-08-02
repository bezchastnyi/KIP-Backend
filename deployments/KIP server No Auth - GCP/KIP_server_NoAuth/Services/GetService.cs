using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KIP_Backend.Models.Helpers;
using KIP_Backend.Models.NoAuth;
using KIP_server_NoAuth.V1.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace KIP_server_NoAuth.Services
{
    /// <summary>
    /// Db service.
    /// </summary>
    public static class GetService
    {
        /// <summary>
        /// Db service.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The logger.</param>
        /// <returns>Task.</returns>
        public static async Task<(
            HashSet<Faculty> facultyList,
            HashSet<Group> groupList,
            HashSet<Cathedra> cathedraList,
            HashSet<Building> buildingList,
            HashSet<Audience> audienceList,
            HashSet<Prof> profList,
            HashSet<StudentSchedule> studentScheduleList,
            HashSet<StudentSchedule> studentSchedule2List,
            HashSet<ProfSchedule> profScheduleList,
            HashSet<ProfSchedule> profSchedule2List,
            HashSet<AudienceSchedule> audienceScheduleList,
            HashSet<AudienceSchedule> audienceSchedule2List)>
            GetAllDataAsync(ILogger<DbUpdateController> logger, IMapper mapper)
        {
            var kipFacultyList = await MapService.GetFacultiesAsync(logger, mapper);
            var kipGroupListByFaculty = await MapService.GetGroupsAsync(kipFacultyList, logger, mapper);
            var kipCathedraListByFaculty = await MapService.GetCathedrasAsync(kipFacultyList, logger, mapper);
            var kipBuildingList = await MapService.GetBuildingsAsync(logger, mapper);
            var kipAudienceListByBuilding = await MapService.GetAudiencesAsync(kipBuildingList, logger, mapper);
            var kipProfListByCathedra = await MapService.GetProfsAsync(kipCathedraListByFaculty, logger, mapper);

            DbUpdateController.Week = Week.UnPaired;
            var kipScheduleByProf = await MapService.GetScheduleByProfAsync(kipProfListByCathedra, logger, mapper);
            var kipScheduleByAudience = await MapService.GetScheduleByAudienceAsync(kipAudienceListByBuilding, logger, mapper);
            var kipScheduleByGroup = await MapService.GetScheduleByGroupAsync(kipGroupListByFaculty, logger, mapper);

            DbUpdateController.Week = Week.Paired;
            var kipSchedule2ByProf = await MapService.GetSchedule2ByProfAsync(kipProfListByCathedra, logger, mapper);
            var kipSchedule2ByAudience = await MapService.GetSchedule2ByAudienceAsync(kipAudienceListByBuilding, logger, mapper);
            var kipSchedule2ByGroup = await MapService.GetSchedule2ByGroupAsync(kipGroupListByFaculty, logger, mapper);

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
            var connectionString = config.GetConnectionString("PostgresConnection");
            await using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            if (connection.State.ToString() == "Open")
            {
                logger.Log(LogLevel.Information, "Connection opened");
                await using var command = new NpgsqlCommand(
                    $"TRUNCATE TABLE " +
                    $"\"{nameof(Audience)}\", " +
                    $"\"{nameof(Building)}\", " +
                    $"\"{nameof(Cathedra)}\", " +
                    $"\"{nameof(Faculty)}\", " +
                    $"\"{nameof(Group)}\", " +
                    $"\"{nameof(Prof)}\", " +
                    $"\"{nameof(AudienceSchedule)}\", " +
                    $"\"{nameof(ProfSchedule)}\", " +
                    $"\"{nameof(StudentSchedule)}\";", connection);

                logger.Log(LogLevel.Information, $"Executing query: {command.CommandText}");
                await command.ExecuteNonQueryAsync();

                logger.Log(LogLevel.Information, "DataBase is cleaned");
                return;
            }

            logger.Log(LogLevel.Error, "Unable to open connection to DB");
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
            var connectionString = config.GetConnectionString("PostgresConnection");
            await using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            if (connection.State.ToString() == "Open")
            {
                logger.Log(LogLevel.Information, "Connection opened");

                await using var command = new NpgsqlCommand($"TRUNCATE TABLE \"{tableName}\";", connection);
                logger.Log(LogLevel.Information, $"Executing query: {command.CommandText}");

                await command.ExecuteNonQueryAsync();
                logger.Log(LogLevel.Information, "DataBase is cleaned");
            }

            logger.Log(LogLevel.Error, "Unable to open connection to DB");
        }
    }
}
