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
    /// Cathedra controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class CathedraController : Controller
    {
        private readonly NoAuthDbContext _context;
        private readonly ILogger<CathedraController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CathedraController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public CathedraController(ILogger<CathedraController> logger, NoAuthDbContext context)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Department.
        /// </summary>
        /// <returns>Department.</returns>
        [HttpGet]
        [Route("Cathedra")]
        [ProducesResponseType(typeof(Cathedra), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Cathedra()
        {
            if (this._context.Cathedra != null)
            {
                return new JsonResult(this._context.Cathedra.AsNoTracking());
            }

            this._logger.LogError($"{nameof(KIP_Backend.Models.NoAuth.Cathedra)} table is empty");
            return this.NotFound();
        }

        /// <summary>
        /// Department.
        /// </summary>
        /// <returns>Department.</returns>
        /// <param name="id">Department ID.</param>
        [HttpGet]
        [Route("Cathedra/{id:int}")]
        [ProducesResponseType(typeof(Cathedra), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Cathedra(int id)
        {
            if (this._context.Cathedra != null)
            {
                var list = this._context.Cathedra.Where(i => i.CathedraId == id).AsNoTracking().ToHashSet();
                return new JsonResult(list);
            }

            this._logger.LogError($"{nameof(KIP_Backend.Models.NoAuth.Cathedra)} table is empty");
            return this.NotFound();
        }

        /// <summary>
        /// Department by faculty.
        /// </summary>
        /// <returns>Department.</returns>
        /// <param name="id">Faculty ID.</param>
        [HttpGet]
        [Route("Cathedra/Faculty/{id:int}")]
        [ProducesResponseType(typeof(Cathedra), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Faculty(int id)
        {
            if (this._context.Cathedra != null)
            {
                var list = this._context.Cathedra.Where(i => i.FacultyId == id).AsNoTracking().ToHashSet();
                return new JsonResult(list);
            }

            this._logger.LogError($"{nameof(KIP_Backend.Models.NoAuth.Cathedra)} table is empty");
            return this.NotFound();
        }
    }
}
