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
    /// Default controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Controller]
    public class CathedraController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CathedraController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public CathedraController(ILogger<HomeController> logger, ServerContext context)
        {
            this._context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// All cathedras.
        /// </summary>
        /// <returns>All cathedras.</returns>
        [HttpGet]
        [Route("Cathedra")]
        public IActionResult Cathedra()
        {
            if (this._context.Cathedra != null)
            {
                return new JsonResult(this._context.Cathedra.AsNoTracking());
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this._logger.Log(LogLevel.Error, message);

            return this.NotFound();
        }

        /// <summary>
        /// Department.
        /// </summary>
        /// <returns>Department.</returns>
        /// <param name="id">Department ID.</param>
        [HttpGet]
        [Route("Cathedra/{id:int}")]
        public IActionResult Cathedra(int id)
        {
            if (this._context.Cathedra != null)
            {
                var list = this._context.Cathedra.Where(i => i.CathedraID == id).AsNoTracking().ToHashSet();

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
        /// Department by faculty.
        /// </summary>
        /// <returns>Department.</returns>
        /// <param name="id">Faculty ID.</param>
        [HttpGet]
        [Route("Cathedra/Faculty/{id:int}")]
        public IActionResult Faculty(int id)
        {
            if (this._context.Cathedra != null)
            {
                var list = this._context.Cathedra.Where(i => i.FacultyID == id).AsNoTracking().ToHashSet();

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
