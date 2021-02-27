using KIP_server_GET.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;

namespace KIP_server_GET.Controllers
{
    /// <summary>
    /// Default controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("/[controller]/[action]")]
    public class FacultyController : Controller
    {
        public IConfiguration Configuration { get; }
        private readonly IFaculty _faculty;

        public FacultyController(IFaculty _iFaculty, IConfiguration configuration)
        {
            _faculty = _iFaculty;
            Configuration = configuration;
        }

        /// <summary>
        /// All Faculties
        /// </summary>
        [HttpGet]
        [Route("/Faculty")]
        public IActionResult Index()
        {
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


            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            var faculties = _faculty.AllFaculties;

            var jsonString = JsonSerializer.Serialize(faculties, options);
            jsonString = Regex.Replace(jsonString, @"\\u([0-9A-Fa-f]{4})", m => "" + (char)Convert.ToInt32(m.Groups[1].Value, 16));

            return this.Ok(jsonString);
        }
    }
}
