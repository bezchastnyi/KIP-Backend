// <copyright file="DebtListController.cs" company="KIP">
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
    /// Debt List controller.
    /// </summary>
    [V1]
    [ApiRoute]
    [ApiController]
    public class DebtListController : Controller
    {
        private readonly ILogger<DebtListController> _logger;
        private readonly IMapper _mapper;
        private readonly IDeserializeService _deserializeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebtListController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="deserializeService">The deserializeService.</param>
        public DebtListController(
            ILogger<DebtListController> logger, IMapper mapper, IDeserializeService deserializeService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._deserializeService = deserializeService ?? throw new ArgumentNullException(nameof(deserializeService));
        }

        /// <summary>
        /// Debt list of student.
        /// </summary>
        /// <param name="email">The email of student.</param>
        /// <param name="password">The password of student.</param>
        /// <returns>Action Result.</returns>
        [HttpGet]
        [Route("DebtList/{email}/{password}")]
        [ProducesResponseType(typeof(DebtList), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DebtList(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(email);
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(password);
            }

            var path = $"{CustomNames.StudentCabinetUrl}email={email}&pass={password}&{CustomNames.DebtListPage}";
            List<DebtList> debtList = null;

            try
            {
                var debtListKHPI = await this._deserializeService.ExecuteAsync<DebtListKHPI>(path);
                if (debtListKHPI == null)
                {
                    this._logger.LogRetrieveDataFromKhPIDbError(ActionNames.RetrieveDataFromKhPIDb, email, password);
                    return this.BadRequest();
                }

                debtList = this._mapper.Map<List<DebtList>>(debtListKHPI);
                if (debtList?.Count == 0)
                {
                    this._logger.LogMapDataError(ActionNames.MapData, email, password);
                    return this.BadRequest();
                }

                this._logger.LogDataGetSuccess(ActionNames.GetDebtList, email, password);
                return new JsonResult(debtList);
            }
            catch (Exception ex)
            {
                this._logger.LogGetDataUnexpectedError(ActionNames.GetDebtList, email, password, ex);
                return this.BadRequest();
            }
        }
    }
}
