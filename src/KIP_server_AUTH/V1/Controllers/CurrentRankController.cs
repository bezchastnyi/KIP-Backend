// <copyright file="CurrentRankController.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using AutoMapper;
using KIP_Backend.Attributes;
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
    /// Current Rank controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class CurrentRankController : Controller
    {
        private readonly ILogger<CurrentRankController> logger;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentRankController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public CurrentRankController(ILogger<CurrentRankController> logger, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Current rank of student's group.
        /// </summary>
        /// <returns>Current rank.</returns>
        /// <param name="email">Email of student.</param>
        /// <param name="password">Password of student.</param>
        [HttpGet]
        [Route("CurrentRank/{email}/{password}")]
        [ProducesResponseType(typeof(CurrentRank), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public IActionResult CurrentRank(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var path = $"{CustomNames.StudentCabinetUrl}email={email}&pass={password}&{CustomNames.CurrentRankPage}";
                var currentRankKHPI = JsonToModelConverter.GetJsonData<CurrentRankKHPI>(path);

                List<CurrentRank> currentRank = null;
                if (currentRankKHPI == null)
                {
                    this.logger.Log(LogLevel.Error, "Error");
                    return this.BadRequest();
                }
                else
                {
                    currentRank = this.mapper.Map<List<CurrentRank>>(currentRankKHPI);
                }

                if (currentRank.Count == 0)
                {
                    return this.BadRequest();
                }

                return new JsonResult(currentRank);
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this.logger.Log(LogLevel.Error, message);

            return this.BadRequest();
        }
    }
}
