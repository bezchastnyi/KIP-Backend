using KIP_server_GET.Constants;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net;

namespace KIP_server_GET.Controllers
{
    /// <summary>
    /// Default controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger</exception>
        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Default action.
        /// </summary>
        [HttpGet]
        [Route("")]
        [Route("/")]
        public IActionResult Index()
        {
            var info = $"{CustomNames.KIP_server_GET} version: {CustomNames.Version}";

            return this.Ok(info);
        }

        /// <summary>
        /// Error action.
        /// </summary>
        [HttpGet]
        [Route("/Home/Error")]
        public IActionResult Error()
        {
            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            _logger.Log(LogLevel.Error, message);

            return new ObjectResult(new { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier }) { StatusCode = (int)HttpStatusCode.BadRequest };
        }
    }
}
