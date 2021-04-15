// <copyright file="KIP_POST_APPHostedService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KIP_POST_APP
{
    /// <summary>
    /// Hosted Servise KIP.
    /// </summary>
    public class KIP_POST_APPHostedService : BackgroundService
    {
        /// <summary>
        /// Gets or sets the week.
        /// </summary>
        /// <value>Week.</value>
        public static Week Week { get; set; }

        private readonly ILogger<KIP_POST_APPHostedService> logger;
        private readonly IHostApplicationLifetime appLifetime;
        private readonly IMapper mapper;
        private readonly ServerContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="KIP_POST_APPHostedService"/> class.
        /// </summary>
        /// <param name="appLifetime">The app life time.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="context">The context.</param>
        public KIP_POST_APPHostedService(
            IHostApplicationLifetime appLifetime, ILogger<KIP_POST_APPHostedService> logger, IMapper mapper, ServerContext context)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.appLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        [Obsolete]
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var message = $"{CustomNames.KIP_POST_APP} version: {CustomNames.Version}";
            this.logger.Log(LogLevel.Information, message);

            try
            {
                var dataList = await GetData(this.logger, this.mapper, cancellationToken);

                /*
                PostData.SendFacultyDataToDB(this._context, DataList.facultyList);
                PostData.SendCathedraDataToDB(this._context, DataList.cathedraList);
                PostData.SendGroupDataToDB(this._context, DataList.groupList);
                PostData.SendBuildingDataToDB(this._context, DataList.buildingList);
                PostData.SendAudienceDataToDB(this._context, DataList.audienceList);
                PostData.SendProfDataToDB(this._context, DataList.profList);
                PostData.SendStudentScheduleDataToDB(this._context, DataList.studentScheduleList);
                PostData.SendStudentScheduleDataToDB(this._context, DataList.studentSchedule2List);
                PostData.SendProfScheduleDataToDB(this._context, DataList.profScheduleList);
                PostData.SendProfScheduleDataToDB(this._context, DataList.profSchedule2List);
                this._context.SaveChanges();
                */

                PostData.PostDataToDB(this.context, dataList);
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

        [Obsolete]
        private static async Task<(List<Faculty> facultyList, List<Group> groupList, List<Cathedra> cathedraList,
                              List<Building> buildingList, List<Audience> audienceList, List<Prof> profList,
                              List<StudentSchedule> studentScheduleList, List<StudentSchedule> studentSchedule2List,
                              List<ProfSchedule> profScheduleList, List<ProfSchedule> profSchedule2List)>
        GetData(ILogger<KIP_POST_APPHostedService> logger, IMapper mapper, CancellationToken cancellationToken)
        {
            var kipFacultyList = await MappedDataToKIPDB.GetFacultyListKIPAsync(logger, mapper, cancellationToken);
            var kipGroupListByFaculty = await MappedDataToKIPDB.GetGroupListByFacultyKIPAsync(kipFacultyList, logger, mapper, cancellationToken);
            var kipCathedraListByFaculty = await MappedDataToKIPDB.GetCathedraListByFacultyKIPAsync(kipFacultyList, logger, mapper, cancellationToken);
            var kipBuildingList = await MappedDataToKIPDB.GetBuildingListKIPAsync(logger, mapper, cancellationToken);
            var kipAudienceListByBuilding = await MappedDataToKIPDB.GetAudienceListByBuildingKIPAsync(kipBuildingList, logger, mapper, cancellationToken);
            var kipProfListByCathedra = await MappedDataToKIPDB.GetProfListByCathedraKIPAsync(kipCathedraListByFaculty, logger, mapper, cancellationToken);
            Week = Week.UnPaired;
            var kipScheduleByGroup = await MappedDataToKIPDB.GetScheduleListByGroupAsync(kipGroupListByFaculty, logger, mapper, cancellationToken);
            Week = Week.Paired;
            var kipSchedule2ByGroup = await MappedDataToKIPDB.GetSchedule2ListByGroupAsync(kipGroupListByFaculty, logger, mapper, cancellationToken);
            Week = Week.UnPaired;
            var kipScheduleByProf = await MappedDataToKIPDB.GetScheduleListByProfAsync(kipProfListByCathedra, logger, mapper, cancellationToken);
            Week = Week.Paired;
            var kipSchedule2ByProf = await MappedDataToKIPDB.GetSchedule2ListByProfAsync(kipProfListByCathedra, logger, mapper, cancellationToken);

            return (kipFacultyList, kipGroupListByFaculty, kipCathedraListByFaculty, kipBuildingList, kipAudienceListByBuilding,
                    kipProfListByCathedra, kipScheduleByGroup, kipSchedule2ByGroup, kipScheduleByProf, kipSchedule2ByProf);
        }

        private void StopApplication()
        {
            this.appLifetime.StopApplication();
        }
    }
}
