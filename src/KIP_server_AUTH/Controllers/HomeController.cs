// <copyright file="HomeController.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using KIP_server_AUTH.Constants;
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
        private readonly ILogger<HomeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Default action.
        /// </summary>
        /// <returns>Json.</returns>
        [HttpGet]
        [Route("")]
        [Route("/")]
        public IActionResult Home()
        {
            var info = $"{CustomNames.KIP_server_AUTH} version: {CustomNames.Version}";
            return this.Ok(info);
        }
    }
}
