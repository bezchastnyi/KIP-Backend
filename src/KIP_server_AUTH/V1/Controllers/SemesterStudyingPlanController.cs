// <copyright file="SemesterStudyingPlanController.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    /// Semester Studying Plan controller.
    /// </summary>
    [V1]
    [ApiRoute]
    [ApiController]
    public class SemesterStudyingPlanController : Controller
    {
        private readonly ILogger<SemesterStudyingPlanController> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SemesterStudyingPlanController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public SemesterStudyingPlanController(ILogger<SemesterStudyingPlanController> logger, IMapper mapper)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Semester Studying Plan of student.
        /// </summary>
        /// <param name="email">The email of student.</param>
        /// <param name="password">The password of student.</param>
        /// <param name="semester">The semester.</param>
        /// <returns>Semester Studying Plan.</returns>
        [HttpGet]
        [Route("SemesterStudyingPlan/{email}/{password}/{semester:int}")]
        [ProducesResponseType(typeof(SemesterStudyingPlan), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SemesterStudyingPlan(string email, string password, int semester)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(email);
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(password);
            }

            if (semester < 0 || semester > 12)
            {
                throw new ArgumentException(nameof(semester));
            }

            var path = $"{CustomNames.StudentCabinetUrl}email={email}&pass={password}&{CustomNames.SemesterStudyingPlanPage}&semestr={semester}";
            List<SemesterStudyingPlan> semesterStudyingPlan = null;

            try
            {
                var semesterStudyingPlanKHPI = await JsonDeserializer.ExecuteAsync<SemesterStudyingPlanKHPI>(path);
                if (semesterStudyingPlanKHPI == null)
                {
                    this._logger.LogRetrieveDataFromKhPIDbError(ActionNames.RetrieveDataFromKhPIDb, email, password);
                    return this.BadRequest();
                }

                semesterStudyingPlan = this._mapper.Map<List<SemesterStudyingPlan>>(semesterStudyingPlanKHPI);
                if (semesterStudyingPlan?.Count == 0)
                {
                    this._logger.LogMapDataError(ActionNames.MapData, email, password);
                    return this.BadRequest();
                }

                this._logger.LogDataGetSuccess(ActionNames.GetSemesterStudyingPlan, email, password);
                return new JsonResult(semesterStudyingPlan);
            }
            catch (Exception ex)
            {
                this._logger.LogGetDataUnexpectedError(ActionNames.GetSemesterStudyingPlan, email, password, ex);
                return this.BadRequest();
            }
        }
    }
}
