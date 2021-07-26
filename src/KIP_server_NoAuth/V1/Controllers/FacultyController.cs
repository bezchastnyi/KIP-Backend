using System;
using System.Linq;
using KIP_Backend.Attributes;
using KIP_Backend.DB;
using KIP_Backend.Models.KIP;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KIP_server_NoAuth.V1.Controllers
{
    /// <summary>
    /// Faculty controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class FacultyController : Controller
    {
        private readonly KIPDbContext _context;
        private readonly ILogger<FacultyController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacultyController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public FacultyController(ILogger<FacultyController> logger, KIPDbContext context)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// All faculties.
        /// </summary>
        /// <returns>All faculties.</returns>
        [HttpGet]
        [Route("Faculty")]
        [ProducesResponseType(typeof(Faculty), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Faculty()
        {
            if (this._context.Faculty != null)
            {
                return new JsonResult(this._context.Faculty.AsNoTracking());
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this._logger.Log(LogLevel.Error, message);

            return this.NotFound();
        }

        /// <summary>
        /// Faculty.
        /// </summary>
        /// <returns>Faculty.</returns>
        /// <param name="id">Faculty ID.</param>
        [HttpGet]
        [Route("Faculty/{id:int}")]
        [ProducesResponseType(typeof(Faculty), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public IActionResult Faculty(int id)
        {
            if (this._context.Faculty != null)
            {
                var list = this._context.Faculty.Where(i => i.FacultyID == id).AsNoTracking().ToHashSet();

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
