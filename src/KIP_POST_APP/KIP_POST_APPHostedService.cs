// <copyright file="KIP_POST_APPHostedService.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KIP_POST_APP.Constants;
using KIP_POST_APP.DB;
using KIP_POST_APP.Models.KIP;
using KIP_POST_APP.Models.KIP.Helpers;
using KIP_POST_APP.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace KIP_POST_APP
{
    /// <summary>
    /// Hosted Servise KIP.
    /// </summary>
    public class KIP_POST_APPHostedService : BackgroundService
    {
        private readonly ILogger<KIP_POST_APPHostedService> logger;
        private readonly IHostApplicationLifetime appLifetime;
        private readonly IMapper mapper;
        private readonly ServerContext context;
        private readonly IConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="KIP_POST_APPHostedService"/> class.
        /// </summary>
        /// <param name="appLifetime">The app life time.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="context">The context.</param>
        /// <param name="config">The config.</param>
        public KIP_POST_APPHostedService(
            IHostApplicationLifetime appLifetime,
            ILogger<KIP_POST_APPHostedService> logger,
            IMapper mapper,
            ServerContext context,
            IConfiguration config)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.appLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Gets or sets the week.
        /// </summary>
        /// <value>Week.</value>
        public static Week Week { get; set; }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var message = $"{CustomNames.KIP_POST_APP} version: {CustomNames.Version}";
            this.logger.Log(LogLevel.Information, message);

            try
            {
                await this.CleanDBAsync(this.config);
                var dataList = await GetDataAsync(this.logger, this.mapper, cancellationToken);
                await PostData.PostDataToDBAsync(this.context, dataList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
            }
            finally
            {
                this.StopApplication();
            }
        }

        private static async Task<
            (List<Faculty> facultyList,
            List<Group> groupList,
            List<Cathedra> cathedraList,
            List<Building> buildingList,
            List<Audience> audienceList,
            List<Prof> profList,
            List<StudentSchedule> studentScheduleList,
            List<StudentSchedule> studentSchedule2List,
            List<ProfSchedule> profScheduleList,
            List<ProfSchedule> profSchedule2List)>
            GetDataAsync(
            ILogger<KIP_POST_APPHostedService> logger,
            IMapper mapper,
            CancellationToken cancellationToken)
        {
            var kipFacultyList = await MappedDataToKIPDB.GetFacultyListKIPAsync(
                logger, mapper, cancellationToken);

            var kipGroupListByFaculty = await MappedDataToKIPDB.GetGroupListByFacultyKIPAsync(
                kipFacultyList, logger, mapper, cancellationToken);

            var kipCathedraListByFaculty = await MappedDataToKIPDB.GetCathedraListByFacultyKIPAsync(
                kipFacultyList, logger, mapper, cancellationToken);

            var kipBuildingList = await MappedDataToKIPDB.GetBuildingListKIPAsync(
                logger, mapper, cancellationToken);

            var kipAudienceListByBuilding = await MappedDataToKIPDB.GetAudienceListByBuildingKIPAsync(
                kipBuildingList, logger, mapper, cancellationToken);

            var kipProfListByCathedra = await MappedDataToKIPDB.GetProfListByCathedraKIPAsync(
                kipCathedraListByFaculty, logger, mapper, cancellationToken);

            Week = Week.UnPaired;
            var kipScheduleByProf = await MappedDataToKIPDB.GetScheduleListByProfAsync(
                kipProfListByCathedra, logger, mapper, cancellationToken);

            var kipScheduleByGroup = await MappedDataToKIPDB.GetScheduleListByGroupAsync(
                kipGroupListByFaculty, logger, mapper, cancellationToken);

            Week = Week.Paired;
            var kipSchedule2ByProf = await MappedDataToKIPDB.GetSchedule2ListByProfAsync(
                kipProfListByCathedra, logger, mapper, cancellationToken);

            var kipSchedule2ByGroup = await MappedDataToKIPDB.GetSchedule2ListByGroupAsync(
                kipGroupListByFaculty, logger, mapper, cancellationToken);

            return (kipFacultyList, kipGroupListByFaculty, kipCathedraListByFaculty, kipBuildingList, kipAudienceListByBuilding,
                    kipProfListByCathedra, kipScheduleByGroup, kipSchedule2ByGroup, kipScheduleByProf, kipSchedule2ByProf);
        }

        private void StopApplication()
        {
            this.appLifetime.StopApplication();
        }

        private async Task CleanDBAsync(IConfiguration config)
        {
            var clean = config["DB:Clean"];

            var message = $"DataBase clean option = {clean}";
            this.logger.Log(LogLevel.Information, message);

            if (clean.ToLower() == "true")
            {
                message = "Start cleaning DB...";
                this.logger.Log(LogLevel.Information, message);

                var connectionString = config["ConnectionStrings:PostgresConnection"];

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    message = "Connection opened";
                    this.logger.Log(LogLevel.Information, message);

                    using (var command = new NpgsqlCommand(
                        $"TRUNCATE TABLE " +
                        $"\"{CustomNames.Audience}\", " +
                        $"\"{CustomNames.Building}\", " +
                        $"\"{CustomNames.Cathedra}\", " +
                        $"\"{CustomNames.Faculty}\", " +
                        $"\"{CustomNames.Group}\", " +
                        $"\"{CustomNames.Prof}\", " +
                        $"\"{CustomNames.ProfSchedule}\", " +
                        $"\"{CustomNames.StudentSchedule}\", " +
                        $"\"{CustomNames.MigrationTable}\";", connection))
                    {
                        message = $"Executing query: {command.CommandText}";
                        this.logger.Log(LogLevel.Information, message);

                        await command.ExecuteNonQueryAsync();

                        message = "DataBase is cleaned";
                        this.logger.Log(LogLevel.Information, message);
                    }
                }
            }
        }
    }
}
