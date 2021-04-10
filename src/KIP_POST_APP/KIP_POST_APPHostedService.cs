using AutoMapper;
using KIP_POST_APP.Models.KIP;
using KIP_POST_APP.Services;
using KIP_POST_APP.DB;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KIP_POST_APP.Constants;
using KIP_POST_APP.Models.KIP.Helpers;

namespace KIP_POST_APP
{
    public class KIP_POST_APPHostedService : BackgroundService
    {
        private readonly ILogger<KIP_POST_APPHostedService> _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IMapper _mapper;
        private readonly ServerContext _context;

        public KIP_POST_APPHostedService(IHostApplicationLifetime appLifetime, ILogger<KIP_POST_APPHostedService> logger, 
                                         IMapper mapper, ServerContext context)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._appLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var message = $"{CustomNames.KIP_POST_APP} version: {CustomNames.Version}";
            this._logger.Log(LogLevel.Information, message);

            try
            {
                var DataList = await GetData(this._logger, this._mapper, cancellationToken);

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

                PostData.PostDataToDB(this._context, DataList); 
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

        private void StopApplication()
        {
            this._appLifetime.StopApplication();
        }

        public static Week week;
        private async static Task<(List<Faculty> facultyList, List<Group> groupList, List<Cathedra> cathedraList, 
                              List<Building> buildingList, List<Audience> audienceList, List<Prof> profList, 
                              List<StudentSchedule> studentScheduleList, List<StudentSchedule> studentSchedule2List,
                              List<ProfSchedule> profScheduleList, List<ProfSchedule> profSchedule2List)>
        GetData(ILogger<KIP_POST_APPHostedService> logger, IMapper mapper, CancellationToken cancellationToken)
        {
            var KIPFacultyList = await MappedDataToKIPDB.GetFacultyListKIPAsync(logger, mapper, cancellationToken);
            var KIPGroupListByFaculty = await MappedDataToKIPDB.GetGroupListByFacultyKIPAsync(KIPFacultyList, logger,
                                                                                                mapper, cancellationToken);
            var KIPCathedraListByFaculty = await MappedDataToKIPDB.GetCathedraListByFacultyKIPAsync(KIPFacultyList, logger,
                                                                                                    mapper, cancellationToken);
            var KIPBuildingList = await MappedDataToKIPDB.GetBuildingListKIPAsync(logger, mapper, cancellationToken);
            var KIPAudienceListByBuilding = await MappedDataToKIPDB.GetAudienceListByBuildingKIPAsync(KIPBuildingList, logger,
                                                                                                mapper, cancellationToken);
            var KIPProfListByCathedra = await MappedDataToKIPDB.GetProfListByCathedraKIPAsync(KIPCathedraListByFaculty, logger,
                                                                                                mapper, cancellationToken);
            week = Week.UnPaired;
            var KIPScheduleByGroup = await MappedDataToKIPDB.GetScheduleListByGroupAsync(KIPGroupListByFaculty, logger,
                                                                                         mapper, cancellationToken);
            week = Week.Paired;
            var KIPSchedule2ByGroup = await MappedDataToKIPDB.GetSchedule2ListByGroupAsync(KIPGroupListByFaculty, logger,
                                                                                           mapper, cancellationToken);
            week = Week.UnPaired;
            var KIPScheduleByProf = await MappedDataToKIPDB.GetScheduleListByProfAsync(KIPProfListByCathedra, logger,
                                                                                       mapper, cancellationToken);
            week = Week.Paired;
            var KIPSchedule2ByProf = await MappedDataToKIPDB.GetSchedule2ListByProfAsync(KIPProfListByCathedra, logger,
                                                                                         mapper, cancellationToken);

            return (KIPFacultyList, KIPGroupListByFaculty, KIPCathedraListByFaculty, KIPBuildingList, KIPAudienceListByBuilding,
                    KIPProfListByCathedra, KIPScheduleByGroup, KIPSchedule2ByGroup, KIPScheduleByProf, KIPSchedule2ByProf);
        }

        public static void ClearDB()
        {
            //this
        }
    }
}