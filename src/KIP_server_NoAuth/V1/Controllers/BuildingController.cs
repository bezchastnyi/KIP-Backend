using System;
using System.Linq;
using KIP_Backend.Attributes;
using KIP_Backend.Models.NoAuth;
using KIP_server_NoAuth.DB;
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
        private readonly NoAuthDbContext _context;
        private readonly ILogger<BuildingController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public BuildingController(ILogger<BuildingController> logger, NoAuthDbContext context)
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

            this._logger.LogError($"{nameof(KIP_Backend.Models.NoAuth.Building)} table is empty");
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
        public IActionResult Building(int id)
        {
            if (this._context.Building != null)
            {
                var list = this._context.Building.Where(i => i.BuildingId == id).AsNoTracking().ToHashSet();
                return new JsonResult(list);
            }

            this._logger.LogError($"{nameof(KIP_Backend.Models.NoAuth.Building)} table is empty");
            return this.NotFound();
        }
    }
}
