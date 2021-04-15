using System;
using KIP_POST_APP.DB;
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
    public class FacultyController : Controller
    {
        private readonly ServerContext _context;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacultyController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public FacultyController(ILogger<HomeController> logger, ServerContext context)
        {
            this._context = context;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Faculty.
        /// </summary>
        /// <returns>Faculty.</returns>
        /// <param name="id">Faculty ID.</param>
        [HttpGet]
        [Route("Faculty/{id:int?}")]
        public IActionResult Faculty(int? id)
        {
            if (id != null)
            {
                foreach (var faculty in this._context.Faculty)
                {
                    if (faculty.FacultyID == id)
                    {
                        return new JsonResult(faculty);
                    }
                }

                return this.NotFound();
            }
            else
            {
                if (this._context.Faculty != null)
                {
                    return new JsonResult(this._context.Faculty);
                }

                return this.NotFound();
            }
        }
    }
}
