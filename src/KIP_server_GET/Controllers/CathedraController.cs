using KIP_server_GET.DB;
using KIP_server_GET.Models.KIP;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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

        public CathedraController(ILogger<HomeController> logger, ServerContext context)
        {
            _context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Faculty by <param name="id">
        /// </summary>
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
                return NotFound();
            }
            else
            {
                if (this._context.Cathedra != null)
                {
                    return new JsonResult(this._context.Cathedra);
                }
                return NotFound();
            }
        }

        /// <summary>
        /// Faculty by <param name="id">
        /// </summary>
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
                    return NotFound();
                }
                else
                {
                    return new JsonResult(list);
                }
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            _logger.Log(LogLevel.Error, message);

            return BadRequest();
        }
    }
}
