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
            if (this._context.StudentSchedule != null)
            {
                return new JsonResult(this._context.StudentSchedule);
            }

            return this.NotFound();
        }

        /// <summary>
        /// Schedule by specific group.
        /// </summary>
        /// <returns>Schedule by specific group.</returns>
        /// <param name="id">Group ID.</param>
        [HttpGet]
        [Route("StudentSchedule/Group/{id:int?}")]
        public IActionResult Group(int? id)
        {
            if (id != null)
            {
                var list = new List<StudentSchedule>();
                foreach (var lesson in this._context.StudentSchedule)
                {
                    if (lesson.GroupID == id)
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
