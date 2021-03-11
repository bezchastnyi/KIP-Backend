using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace KIP_server_GET.Extensions
{
    public class JsonOutputFormat
    {
        public static string PrettyJson(string unPrettyJson)
         {
             var options = new JsonSerializerOptions()
             {
                 WriteIndented = true
             };

             var jsonElement = JsonSerializer.Deserialize<JsonElement>(unPrettyJson);

             return JsonSerializer.Serialize(jsonElement, options);
         }
        
    }
}
