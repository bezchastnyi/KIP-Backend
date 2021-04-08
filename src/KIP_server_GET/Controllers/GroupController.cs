using KIP_POST_APP.DB;
using KIP_POST_APP.Models.KIP;
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
    public class GroupController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<HomeController> _logger;

        public GroupController(ILogger<HomeController> logger, ServerContext context)
        {
            _context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Faculty by <param name="id">
        /// </summary>
        [HttpGet]
        [Route("Group/{id:int?}")]
        public IActionResult Group(int? id)
        {
            if (id != null)
            {
                foreach (var group in this._context.Group)
                {
                    if (group.GroupID == id)
                    {
                        return new JsonResult(group);
                    }
                }
                return NotFound();
            }
            else
            {
                if (this._context.Group != null)
                {
                    return new JsonResult(this._context.Group);
                }
                return NotFound();
            }
        }

        /// <summary>
        /// Faculty by <param name="id">
        /// </summary>
        [HttpGet]
        [Route("Group/Faculty/{id:int?}")]
        public IActionResult Faculty(int? id)
        {
            if (id != null)
            {
                var list = new List<Group>();
                foreach (var group in this._context.Group)
                {
                    if (group.FacultyID == id)
                    {
                        list.Add(group);
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

        /// <summary>
        /// Faculty by <param name="id">
        /// </summary>
        [HttpGet]
        [Route("Group/Cathedra/{id:int?}")]
        public IActionResult Cathedra(int? id)
        {
            if (id != null)
            {
                var list = new List<Group>();
                foreach (var group in this._context.Group)
                {
                    if (group.CathedraID == id)
                    {
                        list.Add(group);
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
