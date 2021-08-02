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
    /// Prof controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class ProfController : Controller
    {
        private readonly NoAuthDbContext _context;
        private readonly ILogger<ProfController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public ProfController(ILogger<ProfController> logger, NoAuthDbContext context)
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
        [ProducesResponseType(typeof(Prof), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Prof(int id)
        {
            if (this._context.Prof != null)
            {
                var list = this._context.Prof.Where(i => i.ProfId == id).AsNoTracking().ToHashSet();
                return new JsonResult(list);
            }

            this._logger.LogError($"{nameof(KIP_Backend.Models.NoAuth.Prof)} table is empty");
            return this.NotFound();
        }

        /// <summary>
        /// Teacher by department.
        /// </summary>
        /// <returns>Teacher.</returns>
        /// <param name="id">Department ID.</param>
        [HttpGet]
        [Route("Prof/Cathedra/{id:int}")]
        [ProducesResponseType(typeof(Prof), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Cathedra(int id)
        {
            if (this._context.Prof != null)
            {
                var list = this._context.Prof.Where(i => i.CathedraId == id).AsNoTracking().ToHashSet();
                return new JsonResult(list);
            }

            this._logger.LogError($"{nameof(KIP_Backend.Models.NoAuth.Prof)} table is empty");
            return this.NotFound();
        }
    }
}
