using System;
using System.Linq;
using KIP_Backend.Attributes;
using KIP_Backend.Models.Helpers;
using KIP_Backend.Models.NoAuth;
using KIP_server_NoAuth.DB;
using KIP_server_NoAuth.Models.Output;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KIP_server_NoAuth.V1.Controllers
{
    /// <summary>
    /// Audience Schedule controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class AudienceScheduleController : Controller
    {
        private readonly NoAuthDbContext _context;
        private readonly ILogger<AudienceScheduleController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudienceScheduleController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public AudienceScheduleController(ILogger<AudienceScheduleController> logger, NoAuthDbContext context)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Schedule by specific audience.
        /// </summary>
        /// <returns>Schedule by specific audience.</returns>
        [HttpGet]
        [Route("AudienceSchedule")]
        [ProducesResponseType(typeof(AudienceSchedule), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult AudienceSchedule()
        {
            if (this._context.AudienceSchedule != null)
            {
                return new JsonResult(this._context.AudienceSchedule.AsNoTracking());
            }

            this._logger.LogError($"{nameof(this.AudienceSchedule)} table is empty");
            return this.NotFound();
        }

        /// <summary>
        /// Schedule by specific audience.
        /// </summary>
        /// <returns>Schedule by specific audience.</returns>
        /// <param name="id">Audience ID.</param>
        [HttpGet]
        [Route("AudienceSchedule/Audience/{id:int}")]
        [ProducesResponseType(typeof(AudienceSchedule), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Audience(int id)
        {
            if (this._context.AudienceSchedule != null)
            {
                var list = this._context.AudienceSchedule.Where(i => i.AudienceId == id).AsNoTracking().ToHashSet();
                return new JsonResult(list);
            }

            this._logger.LogError($"{nameof(this.AudienceSchedule)} table is empty");
            return this.NotFound();
        }

        /// <summary>
        /// Schedule by specific audience.
        /// </summary>
        /// <returns>Schedule by specific audience.</returns>
        /// <param name="id">Audience ID.</param>
        /// <param name="day">Number of day.</param>
        [HttpGet]
        [Route("AudienceSchedule/Audience/{id:int}/Day/{day:int}")]
        [ProducesResponseType(typeof(AudienceSchedule), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public IActionResult Audience(int id, int day)
        {
            if (day < 0 || day > 6)
            {
                this._logger.LogError($"{nameof(day)} must be in range limit");
                return this.BadRequest();
            }

            if (this._context.AudienceSchedule != null)
            {
                var list = this._context.AudienceSchedule.Where(i => i.AudienceId == id && i.Day == (Day)day).AsNoTracking().ToHashSet();
                var outList = list.Select(l => new AudienceScheduleOutput
                    {
                        SubjectName = l.SubjectName,
                        Type = l.Type,
                        Number = l.Number,
                        Week = l.Week,
                        GroupNames = l.GroupNames,
                        ProfName = l.ProfName,
                    })
                    .ToList();

                return new JsonResult(outList);
            }

            this._logger.LogError($"{nameof(this.AudienceSchedule)} table is empty");
            return this.NotFound();
        }
    }
}
