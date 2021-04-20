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
    public class ProfController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public ProfController(ILogger<HomeController> logger, ServerContext context)
        {
            this._context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// All teachers.
        /// </summary>
        /// <returns>Teacher.</returns>
        /// <param name="id">Teacher ID.</param>
        [HttpGet]
        [Route("Prof/{id:int?}")]
        public IActionResult Prof(int? id)
        {
            if (id != null)
            {
                foreach (var prof in this._context.Prof)
                {
                    if (prof.ProfID == id)
                    {
                        return new JsonResult(prof);
                    }
                }

                return this.NotFound();
            }
            else
            {
                if (this._context.Prof != null)
                {
                    return new JsonResult(this._context.Prof);
                }

                return this.NotFound();
            }
        }

        /// <summary>
        /// Teacher by department.
        /// </summary>
        /// <returns>Teacher.</returns>
        /// <param name="id">Department ID.</param>
        [HttpGet]
        [Route("Prof/Cathedra/{id:int?}")]
        public IActionResult Cathedra(int? id)
        {
            if (id != null)
            {
                var list = new List<Prof>();
                foreach (var prof in this._context.Prof)
                {
                    if (prof.CathedraID == id)
                    {
                        list.Add(prof);
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
