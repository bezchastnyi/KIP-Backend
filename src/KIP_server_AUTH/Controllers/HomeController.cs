// <copyright file="HomeController.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KIP_server_AUTH.Controllers
{
    /// <summary>
    /// Default controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [Controller]
    [Route("/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger.</exception>
        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Default action.
        /// </summary>
        /// <returns>OK result with server info.</returns>
        [HttpGet]
        [Route("")]
        [Route("/")]
        public OkObjectResult Home()
        {
            var name = Assembly.GetEntryAssembly()?.GetName().Name;
            var version = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            return this.Ok($"{name}: {version}");
        }

        /// <summary>
        /// Error action.
        /// </summary>
        /// <returns>The action result.</returns>
        [Route("/Home/Error")]
        public IActionResult Error()
        {
            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            this._logger.LogError(message);

            return new ObjectResult(new { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier }) { StatusCode = (int)HttpStatusCode.BadRequest };
        }
    }
}
