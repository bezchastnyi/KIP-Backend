using KIP_server_GET.Constants;
using KIP_server_GET.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Diagnostics;
using System.Net;

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
        public IConfiguration Configuration { get; }
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger</exception>
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            Configuration = configuration;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Default action.
        /// </summary>
        [HttpGet]
        [Route("")]
        [Route("/")]
        public IActionResult Home()
        {
            var info = $"{CustomNames.KIP_server_GET} version: {CustomNames.Version}";

            // return JSON
            return this.Ok(info);
        }

        /// <summary>
        /// Default action.
        /// </summary>
        [HttpGet]
        [Route("/health")]
        public IActionResult health()
        {
            string status = CustomNames.unhealthy_status;
            using (NpgsqlConnection connection = new NpgsqlConnection(this.Configuration.GetConnectionString("PostgresConnection")))
            {
                try
                {
                    connection.Open();

                    if (connection.State.ToString() == "Open")
                        status = CustomNames.healthy_status;

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
            health_check._databases.Add(new DataBase(CustomNames.KIP_database, CustomNames.PostgreSQL, this.Configuration.GetConnectionString("PostgresVersion"), status));

            var message = $"{CustomNames.KIP_database} status: {status}";
            _logger.Log(LogLevel.Information, message);

            return Json(health_check);
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
