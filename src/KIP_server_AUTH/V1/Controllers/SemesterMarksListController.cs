// <copyright file="SemesterMarksListController.cs" company="KIP">
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
        /// <returns>Semester Marks List.</returns>
        /// <param name="email">Email of student.</param>
        /// <param name="password">Password of student.</param>
        /// <param name="semester">Number of semester.</param>
        [HttpGet]
        [Route("SemesterMarksList/{email}/{password}/{semester:int}")]
        [ProducesResponseType(typeof(SemesterMarksListKHPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public IActionResult SemesterMarksList(string email, string password, int semester)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && (semester > 0 && semester < 13))
            {
                var path = $"{CustomNames.StudentCabinetUrl}email={email}&pass={password}&{CustomNames.SemesterMarksListPage}&semestr={semester}";
                var semesterMarksListKHPI = JsonToModelConverter.GetJsonData<SemesterMarksListKHPI>(path);

                List<SemesterMarksList> semesterMarksList = null;
                if (semesterMarksListKHPI == null)
                {
                    this.logger.Log(LogLevel.Error, "Error");
                    return this.BadRequest();
                }
                else
                {
                    semesterMarksList = this.mapper.Map<List<SemesterMarksList>>(semesterMarksListKHPI);
                }

                if (semesterMarksList.Count == 0)
                {
                    return this.BadRequest();
                }

                return new JsonResult(semesterMarksList);
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this.logger.Log(LogLevel.Error, message);

            return this.BadRequest();
        }
    }
}
