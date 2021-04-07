using KIP_POST_APP.DB;
using KIP_POST_APP.Models.KIP;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace KIP_POST_APP.Controllers
{
    /// <summary>
    /// Default controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Controller]
    public class ProfController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<HomeController> _logger;

        public ProfController(ILogger<HomeController> logger, ServerContext context)
        {
            _context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Faculty by <param name="id">
        /// </summary>
        [HttpGet]
        [Route("Prof/{id:int?}")]
        public IActionResult Prof(int? id)
        {
            if (id != null)
            {
                foreach (var prof in this._context.Prof)
                {
                    if (prof.ProfID == id)
                    {
                        return new JsonResult(prof);
                    }
                }
                return NotFound();
            }
            else
            {
                if (this._context.Prof != null)
                {
                    return new JsonResult(this._context.Prof);
                }
                return NotFound();
            }
        }

        /// <summary>
        /// Faculty by <param name="id">
        /// </summary>
        [HttpGet]
        [Route("Prof/Cathedra/{id:int?}")]
        public IActionResult Cathedra(int? id)
        {
            if (id != null)
            {
                var list = new List<Prof>();
                foreach (var prof in this._context.Prof)
                {
                    if (prof.CathedraID == id)
                    {
                        list.Add(prof);
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
