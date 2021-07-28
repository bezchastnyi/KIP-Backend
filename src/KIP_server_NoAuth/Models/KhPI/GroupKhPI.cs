namespace KIP_server_NoAuth.Models.KhPI
{
    /// <summary>
    /// Groups KhPI.
    /// </summary>
    public class GroupKhPI
    {
        /// <summary>
        /// Gets or sets the title of groups.
        /// </summary>
        /// <value>title of groups.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public string title { get; set; }

        /// <summary>
        /// Gets or sets the id of groups.
        /// </summary>
        /// <value>ID of groups.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the course of groups.
        /// </summary>
        /// <value>Course of groups.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "KhPI Db")]
        public int course { get; set; }
    }
}
