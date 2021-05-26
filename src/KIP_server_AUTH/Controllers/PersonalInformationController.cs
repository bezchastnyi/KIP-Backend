// <copyright file="PersonalInformationController.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using AutoMapper;
using KIP_auth_mode.Constants;
using KIP_auth_mode.Mapping.Converters;
using KIP_auth_mode.Models.KHPI;
using KIP_auth_mode.Models.KIP;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace KIP_auth_mode.Controllers
{
    /// <summary>
    /// PersonalInformation controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Controller]
    public class PersonalInformationController : Controller
    {
        private const string PersonalInformationPage = "page=1";

        private readonly ILogger<HomeController> logger;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalInformationController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public PersonalInformationController(ILogger<HomeController> logger, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private string Email { get; set; }

        private string Password { get; set; }

        /// <summary>
        /// Personal Information of student.
        /// </summary>
        /// <returns>Personal Information.</returns>
        /// <param name="email">Email of student.</param>
        /// <param name="password">Password of student.</param>
        [HttpGet]
        [Route("PersonalInformation/{email}/{password}")]
        public IActionResult PersonalInformation(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var path = $"{CustomNames.PersonalInformationUrl}email={email}&pass={password}&{PersonalInformationPage}";
                var personalInformationKHPI = JsonToModelConverter.GetJsonData<PersonalInformationKHPI>(path);

                IEnumerable<PersonalInformation> personalInformation = null;
                if (personalInformationKHPI == null)
                {
                    this.logger.Log(LogLevel.Error, "Error");
                    return this.BadRequest();
                }
                else
                {
                    personalInformation = this.mapper.Map<IEnumerable<PersonalInformation>>(personalInformationKHPI);
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
