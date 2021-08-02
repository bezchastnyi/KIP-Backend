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
    /// Audience controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class AudienceController : Controller
    {
        private readonly NoAuthDbContext _context;
        private readonly ILogger<AudienceController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudienceController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public AudienceController(ILogger<AudienceController> logger, NoAuthDbContext context)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Audience by id.
        /// </summary>
        /// <returns>The audience.</returns>
        /// <param name="id">Audience ID.</param>
        [HttpGet]
        [Route("Audience/{id:int}")]
        [ProducesResponseType(typeof(Audience), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Audience(int id)
        {
            if (this._context.Audience != null)
            {
                var list = this._context.Audience.Where(i => i.AudienceId == id).AsNoTracking().ToHashSet();
                return new JsonResult(list);
            }

            this._logger.LogError($"{nameof(KIP_Backend.Models.NoAuth.Audience)} table is empty");
            return this.NotFound();
        }

        /// <summary>
        /// Audiences by building.
        /// </summary>
        /// <returns>Audiences.</returns>
        /// <param name="id">Building ID.</param>
        [HttpGet]
        [Route("Audience/Building/{id:int}")]
        [ProducesResponseType(typeof(Audience), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Building(int id)
        {
            if (this._context.Audience != null)
            {
                var list = this._context.Audience.Where(i => i.BuildingId == id).AsNoTracking().ToHashSet();
                return new JsonResult(list);
            }

            this._logger.LogError($"{nameof(KIP_Backend.Models.NoAuth.Audience)} table is empty");
            return this.NotFound();
        }
    }
}
