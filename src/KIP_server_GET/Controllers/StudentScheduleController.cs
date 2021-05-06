using System;
using System.Collections.Generic;
using System.Linq;
using KIP_POST_APP.DB;
using KIP_POST_APP.Models.KIP.Helpers;
using KIP_server_GET.Constants;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KIP_server_GET.Controllers
{
    /// <summary>
    /// Default controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Controller]
    public class StudentScheduleController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentScheduleController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public StudentScheduleController(ILogger<HomeController> logger, ServerContext context)
        {
            this._context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Schedule by group.
        /// </summary>
        /// <returns>Schedule by group.</returns>
        [HttpGet]
        [Route("StudentSchedule")]
        public IActionResult StudentSchedule()
        {
            var info = $"{CustomNames.StudentSchedule}";
            return this.Ok(info);
        }

        /// <summary>
        /// Schedule by specific group.
        /// </summary>
        /// <returns>Schedule by specific group.</returns>
        /// <param name="id">Group ID.</param>
        [HttpGet]
        [Route("StudentSchedule/Group/{id:int}")]
        public IActionResult Group(int id)
        {
            if (this._context.StudentSchedule != null)
            {
                var list = this._context.StudentSchedule.Where(i => i.GroupID == id).AsNoTracking().ToHashSet();

                if (list.Count == 0)
                {
                    return this.NotFound();
                }
                else
                {
                    return new JsonResult(list);
                }
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this._logger.Log(LogLevel.Error, message);

            return this.BadRequest();
        }

        /// <summary>
        /// Schedule by specific group.
        /// </summary>
        /// <returns>Schedule by specific group.</returns>
        /// <param name="id">Group ID.</param>
        /// <param name="week">Number of Week.</param>
        /// <param name="day">Number of day.</param>
        [HttpGet]
        [Route("StudentSchedule/Group/{id:int}/Week/{week:int}/Day/{day:int}")]
        public IActionResult Group(int id, int week, int day)
        {
            if (this._context.StudentSchedule != null && day >= 0 && day < 5 && (week == 0 || week == 1))
            {
                var list = this._context.StudentSchedule.Where(i => i.GroupID == id && i.Week == (Week)week && i.Day == (Day)day).AsNoTracking().ToHashSet();

                if (list.Count == 0)
                {
                    return this.NotFound();
                }
                else
                {
                    var outList = new List<Output>();
                    foreach (var l in list)
                    {
                        var output = new Output()
                        {
                            SubjectName = l.SubjectName,
                            Type = l.Type,
                            Number = l.Number,
                            AudienceName = l.AudienceName,
                            ProfName = l.ProfName,
                        };
                        outList.Add(output);
                    }

                    return new JsonResult(outList);
                }
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this._logger.Log(LogLevel.Error, message);

            return this.BadRequest();
        }

        private class Output
        {
            public string SubjectName { get; set; }

            public string Type { get; set; }

            public int Number { get; set; }

            public string AudienceName { get; set; }

            public string ProfName { get; set; }
        }
    }
}
