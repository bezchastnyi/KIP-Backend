namespace KIP_Backend.Models.NoAuth.UI
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
        /// Gets or sets TempProfValue.
        /// </summary>
        public int? TempProfValue { get; set; }

        /// <summary>
        /// Gets or sets TempBuildingValue.
        /// </summary>
        public int? TempBuildingValue { get; set; }

        /// <summary>
        /// Gets or sets TempAudienceValue.
        /// </summary>
        public int? TempAudienceValue { get; set; }

        /// <summary>
        /// Gets or sets TempDayValue.
        /// </summary>
        public int? TempDayValue { get; set; }
    }
}
