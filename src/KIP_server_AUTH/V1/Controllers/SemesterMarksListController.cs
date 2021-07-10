// <copyright file="SemesterMarksListController.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KIP_Backend.Attributes;
using KIP_server_AUTH.Constants;
using KIP_server_AUTH.Extensions;
using KIP_server_AUTH.Interfaces;
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
        private readonly ILogger<SemesterMarksListController> _logger;
        private readonly IMapper _mapper;
        private readonly IDeserializeService _deserializeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SemesterMarksListController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="deserializeService">The deserializeService.</param>
        public SemesterMarksListController(
            ILogger<SemesterMarksListController> logger, IMapper mapper, IDeserializeService deserializeService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._deserializeService = deserializeService ?? throw new ArgumentNullException(nameof(deserializeService));
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
        public async Task<IActionResult> SemesterMarksList(string email, string password, int semester)
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

            var path = $"{CustomNames.StudentCabinetUrl}email={email}&pass={password}&{CustomNames.SemesterMarksListPage}&semestr={semester}";
            List<SemesterMarksList> semesterMarksList = null;

            try
            {
                var semesterMarksListKHPI = await this._deserializeService.ExecuteAsync<SemesterMarksListKHPI>(path);
                if (semesterMarksListKHPI == null)
                {
                    this._logger.LogRetrieveDataFromKhPIDbError(ActionNames.RetrieveDataFromKhPIDb, email, password);
                    return this.BadRequest();
                }

                semesterMarksList = this._mapper.Map<List<SemesterMarksList>>(semesterMarksListKHPI);
                if (semesterMarksList?.Count == 0)
                {
                    this._logger.LogMapDataError(ActionNames.MapData, email, password);
                    return this.BadRequest();
                }

                this._logger.LogDataGetSuccess(ActionNames.GetSemesterMarksList, email, password);
                return new JsonResult(semesterMarksList);
            }
            catch (Exception ex)
            {
                this._logger.LogGetDataUnexpectedError(ActionNames.GetSemesterMarksList, email, password, ex);
                return this.BadRequest();
            }
        }
    }
}
