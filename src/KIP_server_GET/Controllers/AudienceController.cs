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
    /// Audience controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [Controller]
    public class AudienceController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<AudienceController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudienceController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public AudienceController(ILogger<AudienceController> logger, ServerContext context)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// All audiences.
        /// </summary>
        /// <returns>All audienses.</returns>
        [HttpGet]
        [Route("Audience")]
        public IActionResult Audience()
        {
            if (this._context.Audience != null)
            {
                return new JsonResult(this._context.Audience.AsNoTracking());
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this._logger.Log(LogLevel.Error, message);

            return this.NotFound();
        }

        /// <summary>
        /// Audience by id.
        /// </summary>
        /// <returns>Audiense.</returns>
        /// <param name="id">Audience ID.</param>
        [HttpGet]
        [Route("Audience/{id:int}")]
        public IActionResult Audience(int id)
        {
            if (this._context.Audience != null)
            {
                var list = this._context.Audience
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
        /// Audiences by building.
        /// </summary>
        /// <returns>Audiences.</returns>
        /// <param name="id">Building ID.</param>
        [HttpGet]
        [Route("Audience/Building/{id:int}")]
        public IActionResult Building(int id)
        {
            if (this._context.Audience != null)
            {
                var list = this._context.Audience.Where(i => i.BuildingID == id).AsNoTracking().ToHashSet();

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
