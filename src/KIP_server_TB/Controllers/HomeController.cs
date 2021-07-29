using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KIP_server_TB.Controllers
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
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
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
            var info = $"{Assembly.GetEntryAssembly().GetName().Name}: " +
                       $"{Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion}";
            return this.Ok(info);
        }
    }
}
