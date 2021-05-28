// <copyright file="DebtListController.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using AutoMapper;
using KIP_server_AUTH.Constants;
using KIP_server_AUTH.Mapping.Converters;
using KIP_server_AUTH.Models.KHPI;
using KIP_server_AUTH.Models.KIP;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace KIP_server_AUTH.Controllers
{
    /// <summary>
    /// Debt List controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [Controller]
    public class DebtListController : Controller
    {
        private const string DebtListPage = "page=3";

        private readonly ILogger<DebtListController> logger;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebtListController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public DebtListController(ILogger<DebtListController> logger, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Debt list of student.
        /// </summary>
        /// <returns>Debt list.</returns>
        /// <param name="email">Email of student.</param>
        /// <param name="password">Password of student.</param>
        [HttpGet]
        [Route("DebtList/{email}/{password}")]
        public IActionResult DebtList(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var path = $"{CustomNames.StudentCabinetUrl}email={email}&pass={password}&{DebtListPage}";
                var debtListKHPI = JsonToModelConverter.GetJsonData<DebtListKHPI>(path);

                IEnumerable<DebtList> debtList = null;
                if (debtListKHPI == null)
                {
                    this.logger.Log(LogLevel.Error, "Error");
                    return this.BadRequest();
                }
                else
                {
                    debtList = this.mapper.Map<IEnumerable<DebtList>>(debtListKHPI);
                }

                return new JsonResult(debtList);
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this.logger.Log(LogLevel.Error, message);

            return this.BadRequest();
        }
    }
}
