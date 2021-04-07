using System.Collections.Generic;

namespace KIP_server_GET.Models
{
    public class HealthCheck
    {
        public List<DataBase> _databases { get; set; }

        public HealthCheck()
        {
            _databases = new List<DataBase>();
        }
    }
}