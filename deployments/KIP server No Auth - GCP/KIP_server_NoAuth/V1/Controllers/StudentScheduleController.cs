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
    /// Student Schedule controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class StudentScheduleController : Controller
    {
        private readonly NoAuthDbContext _context;
        private readonly ILogger<StudentScheduleController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentScheduleController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public StudentScheduleController(ILogger<StudentScheduleController> logger, NoAuthDbContext context)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Schedule by specific group.
        /// </summary>
        /// <returns>Schedule by specific group.</returns>
        [HttpGet]
        [Route("StudentSchedule")]
        [ProducesResponseType(typeof(StudentSchedule), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult StudentSchedule()
        {
            if (this._context.StudentSchedule != null)
            {
                return new JsonResult(this._context.StudentSchedule.AsNoTracking());
            }

            this._logger.LogError($"{nameof(StudentSchedule)} table is empty");
            return this.NotFound();
        }

        /// <summary>
        /// Schedule by specific group.
        /// </summary>
        /// <returns>Schedule by specific group.</returns>
        /// <param name="id">Group ID.</param>
        [HttpGet]
        [Route("StudentSchedule/Group/{id:int}")]
        [ProducesResponseType(typeof(StudentSchedule), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Group(int id)
        {
            if (this._context.StudentSchedule != null)
            {
                var list = this._context.StudentSchedule.Where(i => i.GroupId == id).AsNoTracking().ToHashSet();
                return new JsonResult(list);
            }

            this._logger.LogError($"{nameof(StudentSchedule)} table is empty");
            return this.NotFound();
        }

        /// <summary>
        /// Schedule by specific group.
        /// </summary>
        /// <returns>Schedule by specific group.</returns>
        /// <param name="id">Group ID.</param>
        /// <param name="day">Number of day.</param>
        [HttpGet]
        [Route("StudentSchedule/Group/{id:int}/Day/{day:int}")]
        [ProducesResponseType(typeof(StudentSchedule), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public IActionResult Group(int id, int day)
        {
            if (day < 0 || day > 6)
            {
                this._logger.LogError($"{nameof(day)} must be in range limit");
                return this.BadRequest();
            }

            if (this._context.StudentSchedule != null)
            {
                var list = this._context.StudentSchedule.Where(i => i.GroupId == id && i.Day == (Day)day).AsNoTracking().ToHashSet();
                var outList = list.Select(l => new StudentScheduleOutput
                {
                    SubjectName = l.SubjectName,
                    Type = l.Type,
                    Number = l.Number,
                    Week = l.Week,
                    AudienceName = l.AudienceName,
                    ProfName = l.ProfName,
                })
                .ToList();

                return new JsonResult(outList);
            }

            this._logger.LogError($"{nameof(StudentSchedule)} table is empty");
            return this.NotFound();
        }
    }
}
