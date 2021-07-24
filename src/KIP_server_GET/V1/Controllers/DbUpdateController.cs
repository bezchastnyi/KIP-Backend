using System;
using System.Threading.Tasks;
using AutoMapper;
using KIP_Backend.Attributes;
using KIP_server_GET.DB;
using KIP_server_GET.Models.KIP.Helpers;
using KIP_server_GET.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KIP_server_GET.V1.Controllers
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
                await DbService.CleanDbAsync(this._logger, this._config);
                var dataList = await DbService.GetDataAsync(this._logger, this._mapper);
                await PostData.PostDataToDBAsync(this._context, dataList);

                /*
                var kipFacultyList = await MappedDataToKIPDB.GetFacultyListKIPAsync(
                this.logger, this.mapper, cancellationToken);

                var kipGroupListByFaculty = await MappedDataToKIPDB.GetGroupListByFacultyKIPAsync(
                    kipFacultyList, this.logger, this.mapper, cancellationToken);

                var kipCathedraListByFaculty = await MappedDataToKIPDB.GetCathedraListByFacultyKIPAsync(
                    kipFacultyList, this.logger, this.mapper, cancellationToken);

                var kipBuildingList = await MappedDataToKIPDB.GetBuildingListKIPAsync(
                    this.logger, this.mapper, cancellationToken);

                var kipAudienceListByBuilding = await MappedDataToKIPDB.GetAudienceListByBuildingKIPAsync(
                    kipBuildingList, this.logger, this.mapper, cancellationToken);

                var kipProfListByCathedra = await MappedDataToKIPDB.GetProfListByCathedraKIPAsync(
                    kipCathedraListByFaculty, this.logger, this.mapper, cancellationToken);

                await PostData.SendFacultyDataToDB(this.context, kipFacultyList);
                await PostData.SendCathedraDataToDB(this.context, kipCathedraListByFaculty);
                await PostData.SendGroupDataToDBAsync(this.context, kipGroupListByFaculty);
                await PostData.SendBuildingDataToDB(this.context, kipBuildingList);
                await PostData.SendAudienceDataToDB(this.context, kipAudienceListByBuilding);
                await PostData.SendProfDataToDB(this.context, kipProfListByCathedra);

                // await PostData.SendProfScheduleDataToDB(this.context, kipScheduleByProf);
                await this.context.SaveChangesAsync();
                */
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
