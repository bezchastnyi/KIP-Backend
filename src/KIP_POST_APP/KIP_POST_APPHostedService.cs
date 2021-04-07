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
            try
            {
                var DataList = await GetData(this._logger, this._mapper, cancellationToken);
                //PostData.PostDataToDB(this._context, DataList);
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

        private async static Task<(List<Faculty> facultyList, List<Group> groupList, List<Cathedra> cathedraList, 
                              List<Building> buildingList, List<Audience> audienceList, List<Prof> profList, 
                              List<StudentSchedule> studentScheduleList, List<ProfSchedule> profScheduleList)>
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
            var KIPScheduleByGroup = await MappedDataToKIPDB.GetScheduleListByGroupAsync(KIPGroupListByFaculty, logger,
                                                                                         mapper, cancellationToken);
            var KIPScheduleByProf = await MappedDataToKIPDB.GetScheduleListByProfAsync(KIPProfListByCathedra, logger,
                                                                                         mapper, cancellationToken);

            return (KIPFacultyList, KIPGroupListByFaculty, KIPCathedraListByFaculty, KIPBuildingList, KIPAudienceListByBuilding,
                    KIPProfListByCathedra, KIPScheduleByGroup, KIPScheduleByProf);
        }
    }
}