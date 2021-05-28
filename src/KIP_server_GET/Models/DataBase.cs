namespace KIP_server_GET.Models
{
    /// <summary>
    /// Database.
    /// </summary>
    public class DataBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataBase"/> class.
        /// </summary>
        /// <param name="name">Name of database.</param>
        /// <param name="db_system">Database system.</param>
        /// <param name="version">Version.</param>
        /// <param name="status">Status.</param>
        public DataBase(string name, string db_system, string version, string status)
        {
            this.Name = name;
            this.DbSystem = db_system;
            this.Version = version;
            this.Status = status;
        }

        /// <summary>
        /// Gets or sets the name of database.
        /// </summary>
        /// <value>Name of database.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the database system.
        /// </summary>
        /// <value>Database system.</value>
        public string DbSystem { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>Version.</value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>Status.</value>
        public string Status { get; set; }
    }
}
