using System;
using System.Linq;
using KIP_POST_APP.DB;
using KIP_POST_APP.Models.KIP;
using KIP_server_GET.Attributes;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KIP_server_GET.V1.Controllers
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
        private readonly ServerContext _context;
        private readonly ILogger<CathedraController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CathedraController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public CathedraController(ILogger<CathedraController> logger, ServerContext context)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// All cathedras.
        /// </summary>
        /// <returns>All cathedras.</returns>
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
        [ProducesResponseType(typeof(Cathedra), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(Cathedra), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
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
