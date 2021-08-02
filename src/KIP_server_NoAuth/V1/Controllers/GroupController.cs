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
    /// Group controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly NoAuthDbContext _context;
        private readonly ILogger<GroupController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public GroupController(ILogger<GroupController> logger, NoAuthDbContext context)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Group.
        /// </summary>
        /// <returns>Group.</returns>
        /// <param name="id">Group ID.</param>
        [HttpGet]
        [Route("Group/{id:int}")]
        [ProducesResponseType(typeof(Group), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Group(int id)
        {
            if (this._context.Group != null)
            {
                var list = this._context.Group.Where(i => i.GroupId == id).AsNoTracking().ToHashSet();
                return new JsonResult(list);
            }

            this._logger.LogError($"{nameof(KIP_Backend.Models.NoAuth.Group)} table is empty");
            return this.NotFound();
        }

        /// <summary>
        /// Group by faculty.
        /// </summary>
        /// <returns>Group.</returns>
        /// <param name="id">Faculty ID.</param>
        [HttpGet]
        [Route("Group/Faculty/{id:int}")]
        [ProducesResponseType(typeof(Group), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Faculty(int id)
        {
            if (this._context.Group != null)
            {
                var list = this._context.Group.Where(i => i.FacultyId == id).AsNoTracking().ToHashSet();
                return new JsonResult(list);
            }

            this._logger.LogError($"{nameof(KIP_Backend.Models.NoAuth.Group)} table is empty");
            return this.NotFound();
        }
    }
}
