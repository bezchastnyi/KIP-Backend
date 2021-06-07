// <copyright file="SemesterStudyingPlanController.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using AutoMapper;
using KIP_server_AUTH.Attributes;
using KIP_server_AUTH.Constants;
using KIP_server_AUTH.Mapping.Converters;
using KIP_server_AUTH.Models.KHPI;
using KIP_server_AUTH.Models.KIP;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace KIP_server_AUTH.V1.Controllers
{
    /// <summary>
    /// Semester Studying Plan controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class SemesterStudyingPlanController : Controller
    {
        private const string SemesterStudyingPlanPage = "page=4";

        private readonly ILogger<SemesterStudyingPlanController> logger;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SemesterStudyingPlanController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public SemesterStudyingPlanController(ILogger<SemesterStudyingPlanController> logger, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Semester Studying Plan of student.
        /// </summary>
        /// <returns>Semester Studying Plan.</returns>
        /// <param name="email">Email of student.</param>
        /// <param name="password">Password of student.</param>
        /// <param name="semester">Number of semester.</param>
        [HttpGet]
        [Route("SemesterStudyingPlan/{email}/{password}/{semester:int}")]
        [ProducesResponseType(typeof(SemesterStudyingPlan), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public IActionResult SemesterStudyingPlan(string email, string password, int semester)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && (semester > 0 && semester < 13))
            {
                var path = $"{CustomNames.StudentCabinetUrl}email={email}&pass={password}&{SemesterStudyingPlanPage}&semestr={semester}";
                var semesterStudyingPlanKHPI = JsonToModelConverter.GetJsonData<SemesterStudyingPlanKHPI>(path);

                List<SemesterStudyingPlan> semesterStudyingPlan = null;
                if (semesterStudyingPlanKHPI == null)
                {
                    this.logger.Log(LogLevel.Error, "Error");
                    return this.BadRequest();
                }
                else
                {
                    semesterStudyingPlan = this.mapper.Map<List<SemesterStudyingPlan>>(semesterStudyingPlanKHPI);
                }

                if (semesterStudyingPlan.Count == 0)
                {
                    return this.BadRequest();
                }

                return new JsonResult(semesterStudyingPlan);
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this.logger.Log(LogLevel.Error, message);

            return this.BadRequest();
        }
    }
}
