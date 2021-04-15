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
    public class BuildingController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public BuildingController(ILogger<HomeController> logger, ServerContext context)
        {
            this._context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// All buildings.
        /// </summary>
        /// <returns>All buildings.</returns>
        [HttpGet]
        [Route("/Building")]
        public IActionResult Building()
        {
            if (this._context.Building != null)
            {
                var buildings = this._context.Building;
                return new JsonResult(buildings);
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this._logger.Log(LogLevel.Error, message);

            return this.NotFound();
        }

        /// <summary>
        /// Building.
        /// </summary>
        /// <returns>Building.</returns>
        /// <param name="id">Building ID.</param>
        [HttpGet]
        [Route("/Building/{id:int?}")]
        public IActionResult Building(int? id)
        {
            if (id != null)
            {
                var buildings = this._context.Building;
                foreach (var unit in buildings)
                {
                    if (unit.BuildingID == id)
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
