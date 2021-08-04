using System;
using System.Threading.Tasks;
using AutoMapper;
using KIP_Backend.Attributes;
using KIP_Backend.Models.Helpers;
using KIP_Backend.Models.NoAuth;
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
        [Route("UpdateDb")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDb()
        {
            try
            {
                this._logger.LogInformation($"Action: '{nameof(this.UpdateDb)}': Start Cleaning Db");
                var result = await PrepareService.CleanDbAsync(this._logger, nameof(this.UpdateDb), this._config);
                if (!result)
                {
                    return this.BadRequest();
                }

                this._logger.LogInformation($"Action: '{nameof(this.UpdateDb)}': Start preparing data");
                var dataList = await PrepareService.PrepareAsync(this._logger, this._mapper);
                this._logger.LogInformation($"Action: '{nameof(this.UpdateDb)}': Data prepared successfully");

                this._logger.LogInformation($"Action: '{nameof(this.UpdateDb)}': Start sending data");
                await SendService.SendDataToDbAsync(this._context, dataList);
                this._logger.LogInformation($"Action: '{nameof(this.UpdateDb)}': Data sent successfully");
            }
            catch
            {
                this._logger.LogInformation($"Action: '{nameof(this.UpdateDb)}': Unexpected error");
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}
