﻿using System.Collections.Generic;

namespace KIP_server_GET.Models
{
    /// <summary>
    /// Health check.
    /// </summary>
    public class HealthCheck
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthCheck"/> class.
        /// </summary>
        public HealthCheck()
        {
            this.Databases = new List<DataBase>();
        }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>Database.</value>
        public List<DataBase> Databases { get; set; }
    }
}
