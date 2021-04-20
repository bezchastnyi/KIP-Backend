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
    public class GroupController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public GroupController(ILogger<HomeController> logger, ServerContext context)
        {
            this._context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Group.
        /// </summary>
        /// <returns>Group.</returns>
        /// <param name="id">Group ID.</param>
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

                return this.NotFound();
            }
            else
            {
                if (this._context.Group != null)
                {
                    return new JsonResult(this._context.Group);
                }

                return this.NotFound();
            }
        }

        /// <summary>
        /// Group by faculty.
        /// </summary>
        /// <returns>Group.</returns>
        /// <param name="id">Faculty ID.</param>
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
