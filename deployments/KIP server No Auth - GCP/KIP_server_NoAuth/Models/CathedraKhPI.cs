namespace KIP_server_NoAuth.Models.KhPI
{
    /// <summary>
    /// Departments KhPI.
    /// </summary>
    public class CathedraKhPI
    {
        /// <summary>
        /// Gets or sets the title of departments.
        /// </summary>
        /// <value>title of departments.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string title { get; set; }

        /// <summary>
        /// Gets or sets the id of departments.
        /// </summary>
        /// <value>ID of departments.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public int id { get; set; }
    }
}
