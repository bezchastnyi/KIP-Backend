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
    /// Prof Schedule controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class ProfScheduleController : Controller
    {
        private readonly NoAuthDbContext _context;
        private readonly ILogger<ProfScheduleController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfScheduleController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public ProfScheduleController(ILogger<ProfScheduleController> logger, NoAuthDbContext context)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Schedule by specific prof.
        /// </summary>
        /// <returns>Schedule by specific teacher.</returns>
        [HttpGet]
        [Route("ProfSchedule")]
        [ProducesResponseType(typeof(ProfSchedule), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult ProfSchedule()
        {
            if (this._context.ProfSchedule != null)
            {
                return new JsonResult(this._context.ProfSchedule.AsNoTracking());
            }

            this._logger.LogError($"{nameof(this.ProfSchedule)} table is empty");
            return this.NotFound();
        }

        /// <summary>
        /// Schedule by specific prof.
        /// </summary>
        /// <returns>Schedule by specific teacher.</returns>
        /// <param name="id">Teacher ID.</param>
        [HttpGet]
        [Route("ProfSchedule/Prof/{id:int}")]
        [ProducesResponseType(typeof(ProfSchedule), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Prof(int id)
        {
            if (this._context.ProfSchedule != null)
            {
                var list = this._context.ProfSchedule.Where(i => i.ProfId == id).AsNoTracking().ToHashSet();
                return new JsonResult(list);
            }

            this._logger.LogError($"{nameof(this.ProfSchedule)} table is empty");
            return this.NotFound();
        }

        /// <summary>
        /// Schedule by specific prof.
        /// </summary>
        /// <returns>Schedule by specific group.</returns>
        /// <param name="id">Group ID.</param>
        /// <param name="day">Number of day.</param>
        [HttpGet]
        [Route("ProfSchedule/Prof/{id:int}/Day/{day:int}")]
        [ProducesResponseType(typeof(ProfSchedule), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public IActionResult Prof(int id, int day)
        {
            if (day < 0 || day > 6)
            {
                this._logger.LogError($"{nameof(day)} must be in range limit");
                return this.BadRequest();
            }

            if (this._context.ProfSchedule != null)
            {
                var list = this._context.ProfSchedule.Where(i => i.ProfId == id && i.Day == (Day)day).AsNoTracking().ToHashSet();
                var outList = list.Select(l => new ProfScheduleOutput
                {
                    SubjectName = l.SubjectName,
                    Type = l.Type,
                    Number = l.Number,
                    Week = l.Week,
                    AudienceName = l.AudienceName,
                    GroupNames = l.GroupNames,
                })
                .ToList();

                return new JsonResult(outList);
            }

            this._logger.LogError($"{nameof(this.ProfSchedule)} table is empty");
            return this.NotFound();
        }
    }
}
