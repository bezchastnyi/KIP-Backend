// <copyright file="SemesterMarksListController.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using AutoMapper;
using KIP_Backend.Attributes;
using KIP_server_AUTH.Constants;
using KIP_server_AUTH.Extensions;
using KIP_server_AUTH.Models.KHPI;
using KIP_server_AUTH.Models.KIP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace KIP_server_AUTH.V1.Controllers
{
    /// <summary>
    /// Semester Marks List controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class SemesterMarksListController : Controller
    {
        private readonly ILogger<SemesterMarksListController> logger;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SemesterMarksListController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public SemesterMarksListController(ILogger<SemesterMarksListController> logger, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Semester Marks List of student.
        /// </summary>
        /// <param name="email">The email of student.</param>
        /// <param name="password">The password of student.</param>
        /// <param name="semester">The semester.</param>
        /// <returns>Semester Marks List.</returns>
        [HttpGet]
        [Route("SemesterMarksList/{email}/{password}/{semester:int}")]
        [ProducesResponseType(typeof(SemesterMarksListKHPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public IActionResult SemesterMarksList(string email, string password, int semester)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && (semester > 0 && semester < 13))
            {
                var path = $"{CustomNames.StudentCabinetUrl}email={email}&pass={password}&{CustomNames.SemesterMarksListPage}&semestr={semester}";
                var semesterMarksListKHPI = JsonDeserializer.ExecuteAsync<SemesterMarksListKHPI>(path);

                List<SemesterMarksList> semesterMarksList = null;
                if (semesterMarksListKHPI == null)
                {
                    // log
                    return this.BadRequest();
                }
                else
                {
                    semesterMarksList = this.mapper.Map<List<SemesterMarksList>>(semesterMarksListKHPI);
                }

                if (semesterMarksList?.Count == 0)
                {
                    return this.BadRequest();
                }

                return new JsonResult(semesterMarksList);
            }

            // log
            return this.BadRequest();
        }
    }
}
