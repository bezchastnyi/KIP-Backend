using System;
using System.Linq;
using KIP_Backend.Attributes;
using KIP_Backend.DB;
using KIP_Backend.Models.KIP.NoAuth;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KIP_server_NoAuth.V1.Controllers
{
    /// <summary>
    /// Building controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class BuildingController : Controller
    {
        private readonly KIPDbContext _context;
        private readonly ILogger<BuildingController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public BuildingController(ILogger<BuildingController> logger, KIPDbContext context)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// All buildings.
        /// </summary>
        /// <returns>All buildings.</returns>
        [HttpGet]
        [Route("Building")]
        [ProducesResponseType(typeof(Building), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Building()
        {
            if (this._context.Building != null)
            {
                return new JsonResult(this._context.Building.AsNoTracking());
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
        [Route("Building/{id:int}")]
        [ProducesResponseType(typeof(Building), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public IActionResult Building(int id)
        {
            if (this._context.Building != null)
            {
                var list = this._context.Building.Where(i => i.BuildingId == id).AsNoTracking().ToHashSet();
                if (list.Count == 0)
                {
                    return this.NotFound();
                }

                return new JsonResult(list);
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this._logger.Log(LogLevel.Error, message);

            return this.BadRequest();
        }
    }
}
