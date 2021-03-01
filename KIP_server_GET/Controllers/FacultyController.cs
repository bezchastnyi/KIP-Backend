using KIP_server_GET.Constants;
using KIP_server_GET.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace KIP_server_GET.Controllers
{
    /// <summary>
    /// Default controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Controller]
    [Route("/[controller]/[action]")]
    public class FacultyController : Controller
    {
        private readonly IFaculty _faculty;
        public IConfiguration Configuration { get; }
        private readonly ILogger<HomeController> _logger;

        public FacultyController(ILogger<HomeController> logger, IFaculty _iFaculty, IConfiguration configuration)
        {
            _faculty = _iFaculty;
            Configuration = configuration;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// All Faculties
        /// </summary>
        [HttpGet]
        [Route("/Faculty")]
        public IActionResult Faculty()
        {
            var info = $"{CustomNames.Faculty} page\n\n";
            info += $"      \'...{CustomNames.Faculty}/All\' --> returns all faculties\n";
            info += $"      \'...{CustomNames.Faculty}/[id]\' --> returns faculty by index\n";

            // return JSON
            return this.Ok(info);
        }

        /// <summary>
        /// All Faculties
        /// </summary>
        [HttpGet]
        [Route("/Faculty/{id:int?}")]
        public JsonResult Faculty(int? id)
        {
            if (id != null)
            {
                var faculties = _faculty.AllFaculties;
                foreach (var f in faculties)
                {
                    if (f.FacultyID == id)
                        return Json(f);
                }

                return Json($"There isn't any faculty with id = {id}");
            }

            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
            _logger.Log(LogLevel.Error, message);

            return Json(message);
        }

        [HttpGet]
        [Route("/Faculty/All")]
        public JsonResult All()
        {
            /*
            using (NpgsqlConnection conn = new NpgsqlConnection(this.Configuration.GetConnectionString("PostgresConnection")))
            {
                try
                {
                    conn.Open();


                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"Faculty\"", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //var faculty_id = reader.GetString(reader.GetOrdinal("FacultyID"));
                            var faculty_name = reader.GetString(reader.GetOrdinal("FacultyName"));
                            Console.WriteLine("faculty_id: " + " -> faculty name = " + faculty_name);
                        }
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            */

            var faculties = _faculty.AllFaculties;

            return Json(faculties);
        }
    }
}
