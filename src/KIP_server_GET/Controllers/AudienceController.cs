using System;
using KIP_POST_APP.DB;
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
    [Route("/[controller]/[action]")]
    public class AudienceController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudienceController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public AudienceController(ILogger<HomeController> logger, ServerContext context)
        {
            this._context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// All audiences.
        /// </summary>
        /// <returns>All audienses.</returns>
        [HttpGet]
        [Route("/Audience")]
        public IActionResult Audience()
        {
            if (this._context.Audience != null)
            {
                var audiences = this._context.Audience;
                return new JsonResult(audiences);
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this._logger.Log(LogLevel.Error, message);

            return this.NotFound();
        }

        /// <summary>
        /// Audiences.
        /// </summary>
        /// <returns>Audienses.</returns>
        /// <param name="id">Audience ID.</param>
        [HttpGet]
        [Route("/Audience/{id:int?}")]
        public IActionResult Audience(int? id)
        {
            if (id != null)
            {
                var audiences = this._context.Audience;
                foreach (var unit in audiences)
                {
                    if (unit.AudienceID == id)
                    {
                        return new JsonResult(unit);
                    }
                }

                return this.NotFound();
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this._logger.Log(LogLevel.Error, message);

            return this.BadRequest();
        }
    }
}
