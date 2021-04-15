using System;
using System.Collections.Generic;
using KIP_POST_APP.DB;
using KIP_POST_APP.Models.KIP;
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
    public class CathedraController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CathedraController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public CathedraController(ILogger<HomeController> logger, ServerContext context)
        {
            this._context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Department.
        /// </summary>
        /// <returns>Department.</returns>
        /// <param name="id">Department ID.</param>
        [HttpGet]
        [Route("Cathedra/{id:int?}")]
        public IActionResult Cathedra(int? id)
        {
            if (id != null)
            {
                foreach (var cathedra in this._context.Cathedra)
                {
                    if (cathedra.CathedraID == id)
                    {
                        return new JsonResult(cathedra);
                    }
                }

                return this.NotFound();
            }
            else
            {
                if (this._context.Cathedra != null)
                {
                    return new JsonResult(this._context.Cathedra);
                }

                return this.NotFound();
            }
        }

        /// <summary>
        /// Department by faculty.
        /// </summary>
        /// <returns>Department.</returns>
        /// <param name="id">Faculty ID.</param>
        [HttpGet]
        [Route("Cathedra/Faculty/{id:int?}")]
        public IActionResult Faculty(int? id)
        {
            if (id != null)
            {
                var list = new List<Cathedra>();
                foreach (var cathedra in this._context.Cathedra)
                {
                    if (cathedra.FacultyID == id)
                    {
                        list.Add(cathedra);
                    }
                }

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
