// <copyright file="PersonalInformationController.cs" company="KIP">
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
    /// Personal Information controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class PersonalInformationController : Controller
    {
        private const string PersonalInformationPage = "page=1";

        private readonly ILogger<PersonalInformationController> logger;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalInformationController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public PersonalInformationController(ILogger<PersonalInformationController> logger, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Personal Information of student.
        /// </summary>
        /// <returns>Personal Information.</returns>
        /// <param name="email">Email of student.</param>
        /// <param name="password">Password of student.</param>
        [HttpGet]
        [Route("PersonalInformation/{email}/{password}")]
        [ProducesResponseType(typeof(PersonalInformation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public IActionResult PersonalInformation(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var path = $"{CustomNames.StudentCabinetUrl}email={email}&pass={password}&{PersonalInformationPage}";
                var personalInformationKHPI = JsonToModelConverter.GetJsonData<PersonalInformationKHPI>(path);

                List<PersonalInformation> personalInformation = null;
                if (personalInformationKHPI == null)
                {
                    this.logger.Log(LogLevel.Error, "Error");
                    return this.BadRequest();
                }
                else
                {
                    personalInformation = this.mapper.Map<List<PersonalInformation>>(personalInformationKHPI);
                }

                if (personalInformation.Count == 0)
                {
                    return this.BadRequest();
                }

                return new JsonResult(personalInformation);
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this.logger.Log(LogLevel.Error, message);

            return this.BadRequest();
        }
    }
}
