﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KIP_Backend.Attributes;
using KIP_Backend.Models.Auth;
using KIP_server_Auth.Constants;
using KIP_server_Auth.Extensions;
using KIP_server_Auth.Interfaces;
using KIP_server_Auth.Models.KhPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KIP_server_Auth.V1.Controllers
{
    /// <summary>
    /// Personal Information controller.
    /// </summary>
    [V1]
    [ApiRoute]
    [ApiController]
    public class PersonalInformationController : Controller
    {
        private readonly ILogger<PersonalInformationController> _logger;
        private readonly IMapper _mapper;
        private readonly IDeserializeService _deserializeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalInformationController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="deserializeService">The deserializeService.</param>
        public PersonalInformationController(
            ILogger<PersonalInformationController> logger, IMapper mapper, IDeserializeService deserializeService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._deserializeService = deserializeService ?? throw new ArgumentNullException(nameof(deserializeService));
        }

        /// <summary>
        /// Personal Information of student.
        /// </summary>
        /// <param name="email">The email of student.</param>
        /// <param name="password">The password of student.</param>
        /// <returns>Personal Information.</returns>
        [HttpGet]
        [Route("PersonalInformation/{email}/{password}")]
        [ProducesResponseType(typeof(PersonalInformation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PersonalInformation(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(email);
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(password);
            }

            var path = $"{CustomNames.StudentCabinetUrl}email={email}&pass={password}&{CustomNames.PersonalInformationPage}";

            try
            {
                var personalInformationKhPI = await this._deserializeService.ExecuteAsync<PersonalInformationKhPI>(path);
                if (personalInformationKhPI == null)
                {
                    this._logger.LogRetrieveDataFromKhPIDbError(ActionNames.RetrieveDataFromKhPIDb, email, password);
                    return this.BadRequest();
                }

                var personalInformation = this._mapper.Map<List<PersonalInformation>>(personalInformationKhPI);
                if (personalInformation?.Count == 0)
                {
                    this._logger.LogMapDataError(ActionNames.MapData, email, password);
                    return this.BadRequest();
                }

                this._logger.LogDataGetSuccess(ActionNames.GetPersonalInformation, email, password);
                return new JsonResult(personalInformation);
            }
            catch (Exception ex)
            {
                this._logger.LogGetDataUnexpectedError(ActionNames.GetPersonalInformation, email, password, ex);
                return this.BadRequest();
            }
        }
    }
}
