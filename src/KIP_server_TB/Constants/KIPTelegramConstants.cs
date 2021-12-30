using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using KIP_Backend.Models.Helpers;

namespace KIP_server_TB.Constants
{
    /// <summary>
    /// KIP Telegram constants.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class KIPTelegramConstants
    {
        /// <summary>
        /// MaxCourse.
        /// </summary>
        public const int MaxCourse = 6;

        /// <summary>
        /// Gets times of lessons.
        /// </summary>
        public static Dictionary<int, string> TimeOfLessonsConstants { get; } = new Dictionary<int, string>
        {
            { 0, "8:30" },
            { 1, "10:25" },
            { 2, "12:35" },
            { 3, "14:30" },
            { 4, "16:25" },
            { 5, "18:10" },
        };

        /// <summary>
        /// Gets days on ukrainian.
        /// </summary>
        public static Dictionary<Day, string> DayUkrConstants { get; } = new Dictionary<Day, string>
        {
            { Day.Monday, "Понеділок" },
            { Day.Tuesday, "Вівторок" },
            { Day.Wednesday, "Середа" },
            { Day.Thursday, "Четвер" },
            { Day.Friday, "П'ятниця" },
        };
    }
}
