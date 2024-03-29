﻿using System;
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
    /// Faculty controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class FacultyController : Controller
    {
        private readonly NoAuthDbContext _context;
        private readonly ILogger<FacultyController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacultyController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public FacultyController(ILogger<FacultyController> logger, NoAuthDbContext context)
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

            this._logger.LogError($"{nameof(KIP_Backend.Models.NoAuth.Faculty)} table is empty");
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
                var list = this._context.Faculty.Where(i => i.FacultyId == id).AsNoTracking().ToHashSet();
                return new JsonResult(list);
            }

            this._logger.LogError($"{nameof(KIP_Backend.Models.NoAuth.Faculty)} table is empty");
            return this.NotFound();
        }
    }
}
