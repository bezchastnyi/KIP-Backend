using AutoMapper;
using KIP_POST_APP.Models.KIPDB;
using KIP_POST_APP.Services;
using KIP_server_GET.DB;
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var KIPFacultyList = await MappedDataToKIPDB.GetFacultyListKIPAsync(this._logger, this._mapper, stoppingToken);
                var KIPGroupListByFaculty = await MappedDataToKIPDB.GetGroupListByFacultyKIPAsync(KIPFacultyList, this._logger,
                                                                                                    this._mapper, stoppingToken);
                var KIPCathedraListByFaculty = await MappedDataToKIPDB.GetCathedraListByFacultyKIPAsync(KIPFacultyList, this._logger,
                                                                                                        this._mapper, stoppingToken);
                var KIPBuildingList = await MappedDataToKIPDB.GetBuildingListKIPAsync(this._logger, this._mapper, stoppingToken);
                var KIPAudienceListByBuilding = await MappedDataToKIPDB.GetAudienceListByBuildingKIPAsync(KIPBuildingList, this._logger,
                                                                                                    this._mapper, stoppingToken);
                var KIPProfListByCathedra = await MappedDataToKIPDB.GetProfListByCathedraKIPAsync(KIPCathedraListByFaculty, this._logger,
                                                                                                    this._mapper, stoppingToken);
                var KIPScheduleByGroup = await MappedDataToKIPDB.GetScheduleListByGroupAsync(KIPGroupListByFaculty, this._logger,
                                                                                             this._mapper, stoppingToken);
                var KIPScheduleByProf = await MappedDataToKIPDB.GetScheduleListByProfAsync(KIPProfListByCathedra, this._logger,
                                                                                             this._mapper, stoppingToken);

                //this.SendFacultyDataToDB(KIPFacultyList);
                //this.SendGroupDataToDB(KIPGroupListByFaculty);


                //this._context.SaveChanges();
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

        private void SendFacultyDataToDB(List<Faculty> objects)
        {
            foreach(var obj in objects)
            {
                this._context.Faculty.Add(obj);
            }
        }

        private void SendGroupDataToDB(List<Group> objects)
        {
            foreach (var obj in objects)
            {
                this._context.Group.Add(obj);
            }
        }

        private void SendCathedraDataToDB(List<Cathedra> objects)
        {
            foreach (var obj in objects)
            {
                this._context.Cathedra.Add(obj);
            }
        }

        private void SendBuildingDataToDB(List<Building> objects)
        {
            foreach (var obj in objects)
            {
                this._context.Building.Add(obj);
            }
        }

        private void SendAudienceDataToDB(List<Audience> objects)
        {
            foreach (var obj in objects)
            {
                this._context.Audience.Add(obj);
            }
        }

        private void SendProfDataToDB(List<Prof> objects)
        {
            foreach (var obj in objects)
            {
                this._context.Prof.Add(obj);
            }
        }

        private void SendStudentScheduleDataToDB(List<StudentSchedule> objects)
        {
            foreach (var obj in objects)
            {
                this._context.StudentSchedule.Add(obj);
            }
        }

        private void SendProfScheduleDataToDB(List<ProfSchedule> objects)
        {
            foreach (var obj in objects)
            {
                this._context.ProfSchedule.Add(obj);
            }
        }
    }
}