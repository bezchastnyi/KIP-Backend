using System;
using KIP_server_GET.Constants;
using KIP_server_GET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace KIP_server_GET.Controllers
{
    /// <summary>
    /// Default controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Controller]
    [Route("/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            this.Configuration = configuration;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets configurations of server.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Default action.
        /// </summary>
        /// <returns>Json.</returns>
        [HttpGet]
        [Route("")]
        [Route("/")]
        public IActionResult Home()
        {
            var info = $"{CustomNames.KIP_server_GET} version: {CustomNames.Version}";
            return this.Ok(info);
        }

        /// <summary>
        /// Check status.
        /// </summary>
        /// <returns>Status.</returns>
        [HttpGet]
        [Route("/health")]
        public IActionResult Health()
        {
            var status = CustomNames.Unhealthy_status;
            using (var connection = new NpgsqlConnection(this.Configuration.GetConnectionString("PostgresConnection")))
            {
                try
                {
                    connection.Open();

                    if (connection.State.ToString() == "Open")
                    {
                        status = CustomNames.Healthy_status;
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            var health_check = new HealthCheck();
            health_check.Databases.Add(new DataBase(CustomNames.KIP_database, CustomNames.PostgreSQL, this.Configuration.GetConnectionString("PostgresVersion"), status));

            var message = $"{CustomNames.KIP_database} status: {status}";
            this._logger.Log(LogLevel.Information, message);

            return this.Json(health_check);
        }
    }
}
