namespace KIP_server_NoAuth.Models.KhPI
{
    /// <summary>
    /// Building KhPI.
    /// </summary>
    public class BuildingKhPI
    {
        /// <summary>
        /// Gets or sets the title of building.
        /// </summary>
        /// <value>title of building.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string title { get; set; }

        /// <summary>
        /// Gets or sets the id of building.
        /// </summary>
        /// <value>ID of building.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public int id { get; set; }
    }
}
