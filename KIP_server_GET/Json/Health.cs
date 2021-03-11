using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace KIP_server_GET.Json
{
     /* var response = "{\"" + $"{CustomNames.KIP_database}" + "\": " +
                            "{\"DB system\": \"" + $"{CustomNames.PostgreSQL}" + "\", " +
                            "\"Version\": \"" + $"{this.Configuration.GetConnectionString("PostgresVersion")}" + "\", " +
                            "\"status\": \"" + $"{status}" + "\"}}";
            */
    public class Health
    {
        public Health()
        {
            Databases = new List<Database>();
        }
        public List<Database> Databases { get; set; }
    }

    public class Database
    {
        public Database(string name, string dB_system, string version, string status)
        {
            Name = name;
            DB_system = dB_system;
            Version = version;
            Status = status;
        }
        public string Name { get; set; }
        [JsonProperty("DB system")]
        public string DB_system { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
    }

}
