using KIP_server_GET.DB;
using KIP_server_GET.Models.KIP;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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

        public StudentScheduleController(ILogger<HomeController> logger, ServerContext context)
        {
            _context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Faculty
        /// </summary>
        [HttpGet]
        [Route("StudentSchedule")]
        public IActionResult StudentSchedule()
        {
            if (this._context.StudentSchedule != null)
            {
                return new JsonResult(this._context.StudentSchedule);
            }
            return NotFound();
        }

        /// <summary>
        /// Faculty by <param name="id">
        /// </summary>
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
                    return NotFound();
                }
                else
                {
                    return new JsonResult(list);
                }
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            _logger.Log(LogLevel.Error, message);

            return BadRequest();
        }
    }
}
