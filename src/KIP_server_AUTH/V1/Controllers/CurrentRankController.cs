// <copyright file="CurrentRankController.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KIP_Backend.Attributes;
using KIP_Backend.Constants;
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
    /// Current Rank controller.
    /// </summary>
    [V1]
    [ApiRoute]
    [ApiController]
    public class CurrentRankController : Controller
    {
        private readonly ILogger<CurrentRankController> _logger;
        private readonly IMapper _mapper;
        private readonly IDeserializeService _deserializeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentRankController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="deserializeService">The deserializeService.</param>
        public CurrentRankController(
            ILogger<CurrentRankController> logger, IMapper mapper, IDeserializeService deserializeService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._deserializeService = deserializeService ?? throw new ArgumentNullException(nameof(deserializeService));
        }

        /// <summary>
        /// Current rank of student's group.
        /// </summary>
        /// <param name="email">The email of student.</param>
        /// <param name="password">The password of student.</param>
        /// <returns>Action Result.</returns>
        [HttpGet]
        [Route("CurrentRank/{email}/{password}")]
        [ProducesResponseType(typeof(CurrentRank), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CurrentRank(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(string.Format(BackendConstants.NullOrEptyErrorMessage, email));
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(string.Format(BackendConstants.NullOrEptyErrorMessage, password));
            }

            var path = $"{CustomNames.StudentCabinetUrl}email={email}&pass={password}&{CustomNames.CurrentRankPage}";
            List<CurrentRank> currentRank = null;

            try
            {
                var currentRankKHPI = await this._deserializeService.ExecuteAsync<CurrentRankKHPI>(path);
                if (currentRankKHPI == null)
                {
                    this._logger.LogRetrieveDataFromKhPIDbError(ActionNames.RetrieveDataFromKhPIDb, email, password);
                    return this.BadRequest();
                }

                currentRank = this._mapper.Map<List<CurrentRank>>(currentRankKHPI);
                if (currentRank?.Count == 0)
                {
                    this._logger.LogMapDataError(ActionNames.MapData, email, password);
                    return this.BadRequest();
                }

                this._logger.LogDataGetSuccess(ActionNames.GetCurrentRank, email, password);
                return new JsonResult(currentRank);
            }
            catch (Exception ex)
            {
                this._logger.LogGetDataUnexpectedError(ActionNames.GetCurrentRank, email, password, ex);
                return this.BadRequest();
            }
        }
    }
}
