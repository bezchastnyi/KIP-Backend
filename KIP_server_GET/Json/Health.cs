using System.Collections.Generic;

namespace KIP_server_GET.Json
{
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
            Db_system = dB_system;
            Version = version;
            Status = status;
        }
        public string Name { get; set; }
        public string Db_system { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
    }

}
