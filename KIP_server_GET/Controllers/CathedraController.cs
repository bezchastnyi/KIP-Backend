using KIP_server_GET.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace KIP_server_GET.Controllers
{
    /// <summary>
    /// Default controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Controller]
    [Route("/[controller]/[action]")]
    public class CathedraController : Controller
    {
        private readonly JsonSerializerOptions options;
        public IConfiguration Configuration { get; }


        public CathedraController(IConfiguration configuration)
        {
            options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            Configuration = configuration;
        }

        /// <summary>
        /// All Cathedras
        /// </summary>
        [HttpGet]
        [Route("/Cathedra")]
        public IActionResult Cathedra()
        {
            var info = $"{CustomNames.Cathedra} page\n\n";
            info += $"      \'...{CustomNames.Cathedra}/All\' --> returns all cathedras\n";
            info += $"      \'...{CustomNames.Cathedra}/[id]\' --> returns cathedra by index\n";

            // return JSON
            return this.Ok(info);
        }

        [HttpGet]
        [Route("/Cathedra/All")]
        public IActionResult All()
        {



            return this.Ok("");
        }
    }
}
