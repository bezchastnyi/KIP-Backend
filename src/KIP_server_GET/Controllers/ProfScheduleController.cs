using KIP_POST_APP.DB;
using KIP_POST_APP.Models.KIP;
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
    public class ProfScheduleController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<HomeController> _logger;

        public ProfScheduleController(ILogger<HomeController> logger, ServerContext context)
        {
            _context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Faculty
        /// </summary>
        [HttpGet]
        [Route("ProfSchedule")]
        public IActionResult ProfSchedule()
        {
            if (this._context.ProfSchedule != null)
            {
                return new JsonResult(this._context.ProfSchedule);
            }
            return NotFound();
        }

        /// <summary>
        /// Faculty by <param name="id">
        /// </summary>
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
