namespace KIP_server_TB.Models
{
    /// <summary>
    /// TelegramUser.
    /// </summary>
    public class TelegramUser
    {
        /// <summary>
        /// Gets or sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets UserName.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets Faculty.
        /// </summary>
        public string Faculty { get; set; }

        /// <summary>
        /// Gets or sets Course.
        /// </summary>
        public int? Course { get; set; }

        /// <summary>
        /// Gets or sets Group.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets Group.
        /// </summary>
        public int? TempDayValue { get; set; }
    }
}
