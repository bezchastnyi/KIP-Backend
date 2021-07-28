using System;
using System.Threading.Tasks;
using AutoMapper;
using KIP_Backend.Attributes;
using KIP_Backend.DB;
using KIP_Backend.Models.KIP.NoAuth;
using KIP_Backend.Models.KIP.NoAuth.Helpers;
using KIP_server_NoAuth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KIP_server_NoAuth.V1.Controllers
{
    /// <summary>
    /// Db update controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class DbUpdateController : Controller
    {
        private readonly KIPDbContext _context;
        private readonly ILogger<DbUpdateController> _logger;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbUpdateController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="config">The config.</param>
        public DbUpdateController(ILogger<DbUpdateController> logger, KIPDbContext context, IConfiguration config, IMapper mapper)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._config = config ?? throw new ArgumentNullException(nameof(config));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets or sets the week.
        /// </summary>
        /// <value>Week.</value>
        public static Week Week { get; set; } = Week.UnPaired;

        /// <summary>
        /// Update KIP Db.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("UpdateAllDb")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAllDb()
        {
            try
            {
                // log
                await GetService.CleanDbAsync(this._logger, this._config);

                /*
                var kipFacultyList = await MapService.GetFacultiesAsync(this._logger, this._mapper);
                var kipGroupListByFaculty = await MapService.GetGroupsAsync(kipFacultyList, this._logger, this._mapper);
                var kipCathedraListByFaculty = await MapService.GetCathedrasAsync(kipFacultyList, this._logger, this._mapper);
                var kipBuildingList = await MapService.GetBuildingsAsync(this._logger, this._mapper);
                var kipAudienceListByBuilding = await MapService.GetAudiencesAsync(kipBuildingList, this._logger, this._mapper);
                var kipProfListByCathedra = await MapService.GetProfsAsync(kipCathedraListByFaculty, this._logger, this._mapper);

                await SendService.SendFacultyDataToDbAsync(this._context, kipFacultyList);
                await SendService.SendGroupDataToDbAsync(this._context, kipGroupListByFaculty);
                await SendService.SendCathedraDataToDbAsync(this._context, kipCathedraListByFaculty);
                await SendService.SendBuildingDataToDbAsync(this._context, kipBuildingList);
                await SendService.SendAudienceDataToDbAsync(this._context, kipAudienceListByBuilding);
                await SendService.SendProfDataToDbAsync(this._context, kipProfListByCathedra);
                */

                // log
                var dataList = await GetService.GetAllDataAsync(this._logger, this._mapper);

                // log
                await SendService.SendDataToDbAsync(this._context, dataList);
            }
            catch
            {
                // log
                return this.BadRequest();
            }

            return this.Ok();
        }

        /// <summary>
        /// Update KIP Db.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("UpdateSchedule")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSchedule()
        {
            try
            {
                // log
                await GetService.CleanTableAsync(this._logger, this._config, nameof(StudentSchedule));
                await GetService.CleanTableAsync(this._logger, this._config, nameof(ProfSchedule));
                await GetService.CleanTableAsync(this._logger, this._config, nameof(AudienceSchedule));

                // log
                var (_, _, _, _, _, _, studentScheduleList, studentSchedule2List,
                    profScheduleList, profSchedule2List,
                    audienceScheduleList, audienceSchedule2List) = await GetService.GetAllDataAsync(this._logger, this._mapper);

                // log
                await SendService.SendStudentScheduleDataToDbAsync(this._context, studentScheduleList);
                await SendService.SendStudentScheduleDataToDbAsync(this._context, studentSchedule2List);

                await SendService.SendProfScheduleDataToDbAsync(this._context, profScheduleList);
                await SendService.SendProfScheduleDataToDbAsync(this._context, profSchedule2List);

                await SendService.SendAudienceScheduleDataToDbAsync(this._context, audienceScheduleList);
                await SendService.SendAudienceScheduleDataToDbAsync(this._context, audienceSchedule2List);
            }
            catch
            {
                // log
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}
