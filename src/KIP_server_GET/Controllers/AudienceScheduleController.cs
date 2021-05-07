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
    public class AudienceScheduleController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudienceScheduleController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public AudienceScheduleController(ILogger<HomeController> logger, ServerContext context)
        {
            this._context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Schedule by audience.
        /// </summary>
        /// <returns>Schedule by audience.</returns>
        [HttpGet]
        [Route("AudienceSchedule")]
        public IActionResult AudienceSchedule()
        {
            var info = $"{CustomNames.AudienceSchedule}";
            return this.Ok(info);
        }

        /// <summary>
        /// Schedule by specific audience.
        /// </summary>
        /// <returns>Schedule by specific audience.</returns>
        /// <param name="id">Audience ID.</param>
        [HttpGet]
        [Route("AudienceSchedule/Audience/{id:int}")]
        public IActionResult Audience(int id)
        {
            if (this._context.AudienceSchedule != null)
            {
                var list = this._context.AudienceSchedule
                    .Where(i => i.AudienceID == id).AsNoTracking().ToHashSet();

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
        /// Schedule by specific audience.
        /// </summary>
        /// <returns>Schedule by specific audience.</returns>
        /// <param name="id">Audience ID.</param>
        /// <param name="day">Number of day.</param>
        [HttpGet]
        [Route("AudienceSchedule/Audience/{id:int}/Day/{day:int}")]
        public IActionResult Audience(int id, int day)
        {
            if (this._context.AudienceSchedule != null && day >= 0 && day < 6)
            {
                var list = this._context.AudienceSchedule
                    .Where(i => i.AudienceID == id && i.Day == (Day)day).AsNoTracking().ToHashSet();

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
                            Week = l.Week,
                            GroupNames = l.GroupNames,
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

            public Week Week { get; set; }

            public string GroupNames { get; set; }

            public string ProfName { get; set; }
        }
    }
}
