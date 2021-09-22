using System;
using System.Threading.Tasks;
using AutoMapper;
using KIP_Backend.Attributes;
using KIP_Backend.Models.Helpers;
using KIP_server_NoAuth.DB;
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
        private readonly NoAuthDbContext _context;
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
        public DbUpdateController(ILogger<DbUpdateController> logger, NoAuthDbContext context, IConfiguration config, IMapper mapper)
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
        [Route("CleanDb")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CleanDb()
        {
            try
            {
                this._logger.LogInformation($"Action: '{nameof(this.CleanDb)}': Start Cleaning Db");
                var result = await PrepareService.CleanDbAsync(this._logger, nameof(this.CleanDb), this._config);
                if (!result)
                {
                    // log
                    return this.BadRequest();
                }

                return this.Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogInformation($"Action: '{nameof(this.CleanDb)}': Unexpected error, message {ex.Message}");
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Update KIP Db.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("UpdateMainStuff")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMainStuff()
        {
            try
            {
                this._logger.LogInformation($"Action: '{nameof(this.UpdateMainStuff)}': Start preparing data");
                var dataList = await PrepareService.PrepareMainStuffAsync(this._logger, this._mapper);
                this._logger.LogInformation($"Action: '{nameof(this.UpdateMainStuff)}': Data prepared successfully");

                this._logger.LogInformation($"Action: '{nameof(this.UpdateMainStuff)}': Start sending data");
                await SendService.SendMainStuffToDbAsync(this._context, dataList);
                this._logger.LogInformation($"Action: '{nameof(this.UpdateMainStuff)}': Data sent successfully");

                return this.Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogInformation($"Action: '{nameof(this.UpdateMainStuff)}': Unexpected error, message {ex.Message}");
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Update KIP Db.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("UpdateStudentSchedule")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudentSchedule()
        {
            try
            {
                this._logger.LogInformation($"Action: '{nameof(this.UpdateStudentSchedule)}': Start preparing data");
                var dataList = await PrepareService.PrepareStudentScheduleAsync(this._logger, this._mapper, this._context);
                this._logger.LogInformation($"Action: '{nameof(this.UpdateStudentSchedule)}': Data prepared successfully");

                this._logger.LogInformation($"Action: '{nameof(this.UpdateStudentSchedule)}': Start sending data");
                await SendService.SendStudentScheduleToDbAsync(this._context, dataList);
                this._logger.LogInformation($"Action: '{nameof(this.UpdateStudentSchedule)}': Data sent successfully");

                return this.Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogInformation($"Action: '{nameof(this.UpdateStudentSchedule)}': Unexpected error, message {ex.Message}");
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Update KIP Db.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("UpdateProfSchedule")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProfSchedule()
        {
            try
            {
                this._logger.LogInformation($"Action: '{nameof(this.UpdateProfSchedule)}': Start preparing data");
                var dataList = await PrepareService.PrepareProfScheduleAsync(this._logger, this._mapper, this._context);
                this._logger.LogInformation($"Action: '{nameof(this.UpdateProfSchedule)}': Data prepared successfully");

                this._logger.LogInformation($"Action: '{nameof(this.UpdateProfSchedule)}': Start sending data");
                await SendService.SendProfScheduleToDbAsync(this._context, dataList);
                this._logger.LogInformation($"Action: '{nameof(this.UpdateProfSchedule)}': Data sent successfully");

                return this.Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogInformation($"Action: '{nameof(this.UpdateProfSchedule)}': Unexpected error, message {ex.Message}");
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Update KIP Db.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("UpdateAudienceSchedule")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAudienceSchedule()
        {
            try
            {
                this._logger.LogInformation($"Action: '{nameof(this.UpdateAudienceSchedule)}': Start preparing data");
                var dataList = await PrepareService.PrepareAudienceScheduleAsync(this._logger, this._mapper, this._context);
                this._logger.LogInformation($"Action: '{nameof(this.UpdateAudienceSchedule)}': Data prepared successfully");

                this._logger.LogInformation($"Action: '{nameof(this.UpdateAudienceSchedule)}': Start sending data");
                await SendService.SendAudienceScheduleToDbAsync(this._context, dataList);
                this._logger.LogInformation($"Action: '{nameof(this.UpdateAudienceSchedule)}': Data sent successfully");

                return this.Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogInformation($"Action: '{nameof(this.UpdateAudienceSchedule)}': Unexpected error, message {ex.Message}");
                return this.BadRequest();
            }
        }
    }
}
