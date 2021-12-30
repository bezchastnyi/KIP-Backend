namespace KIP_server_TB.Models
{
    /// <summary>
    /// Telegram user model.
    /// </summary>
    public class TelegramUser
    {
        /// <summary>
        /// Gets or sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets UserEmail.
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets UserPassword.
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// Gets or sets UserName.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets FacultyId.
        /// </summary>
        public int? FacultyId { get; set; }

        /// <summary>
        /// Gets or sets FacultyName.
        /// </summary>
        public string FacultyName { get; set; }

        /// <summary>
        /// Gets or sets FacultyShortName.
        /// </summary>
        public string FacultyShortName { get; set; }

        /// <summary>
        /// Gets or sets Course.
        /// </summary>
        public int? Course { get; set; }

        /// <summary>
        /// Gets or sets GroupId.
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// Gets or sets GroupName.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets TempProfValue.
        /// </summary>
        public int? TempProfValue { get; set; } // TODO MUST BE IN REDIS

        /// <summary>
        /// Gets or sets TempBuildingValue.
        /// </summary>
        public int? TempBuildingValue { get; set; } // TODO MUST BE IN REDIS

        /// <summary>
        /// Gets or sets TempAudienceValue.
        /// </summary>
        public int? TempAudienceValue { get; set; } // TODO MUST BE IN REDIS

        /// <summary>
        /// Gets or sets TempDayValue.
        /// </summary>
        public int? TempDayValue { get; set; } // TODO MUST BE IN REDIS
    }
}
