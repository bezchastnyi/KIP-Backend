using System;
using System.Collections.Generic;
using KIP_POST_APP.DB;
using KIP_POST_APP.Models.KIP;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace KIP_server_GET.Controllers
{
    /// <summary>
    /// Default controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Controller]
    public class ProfScheduleController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfScheduleController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public ProfScheduleController(ILogger<HomeController> logger, ServerContext context)
        {
            this._context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Schedule by teacher.
        /// </summary>
        /// <returns>Schedule by teacher.</returns>
        [HttpGet]
        [Route("ProfSchedule")]
        public IActionResult ProfSchedule()
        {
            if (this._context.ProfSchedule != null)
            {
                return new JsonResult(this._context.ProfSchedule);
            }

            return this.NotFound();
        }

        /// <summary>
        /// Schedule by specific teacher.
        /// </summary>
        /// <returns>Schedule by specific teacher.</returns>
        /// <param name="id">Teacher ID.</param>
        [HttpGet]
        [Route("ProfSchedule/Prof/{id:int?}")]
        public IActionResult Prof(int? id)
        {
            if (id != null)
            {
                var list = new List<ProfSchedule>();
                foreach (var lesson in this._context.ProfSchedule)
                {
                    if (lesson.ProfID == id)
                    {
                        list.Add(lesson);
                    }
                }

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
    }
}
