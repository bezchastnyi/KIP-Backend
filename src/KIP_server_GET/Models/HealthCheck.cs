using System.Collections.Generic;

namespace KIP_POST_APP.Models
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