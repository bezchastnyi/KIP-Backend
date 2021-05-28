using System;
using System.Linq;
using KIP_POST_APP.DB;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KIP_server_GET.Controllers
{
    /// <summary>
    /// Prof controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [Controller]
    public class ProfController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<ProfController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public ProfController(ILogger<ProfController> logger, ServerContext context)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// All teachers.
        /// </summary>
        /// <returns>Teacher.</returns>
        /// <param name="id">Teacher ID.</param>
        [HttpGet]
        [Route("Prof/{id:int}")]
        public IActionResult Prof(int id)
        {
            if (this._context.Prof != null)
            {
                var list = this._context.Prof.Where(i => i.ProfID == id).AsNoTracking().ToHashSet();

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
        /// Teacher by department.
        /// </summary>
        /// <returns>Teacher.</returns>
        /// <param name="id">Department ID.</param>
        [HttpGet]
        [Route("Prof/Cathedra/{id:int}")]
        public IActionResult Cathedra(int id)
        {
            if (this._context.Prof != null)
            {
                var list = this._context.Prof.Where(i => i.CathedraID == id).AsNoTracking().ToHashSet();

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
