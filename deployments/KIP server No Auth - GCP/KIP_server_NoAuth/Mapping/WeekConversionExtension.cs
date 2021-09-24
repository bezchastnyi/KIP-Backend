using System;
using AutoMapper;
using KIP_Backend.Models.Helpers;

namespace KIP_server_NoAuth.Mapping
{
    /// <summary>
    /// Week conversion extension.
    /// </summary>
    public static class WeekConversionExtension
    {
        private const string WeekKey = "Week";

        /// <summary>
        /// Get week value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="scheduleType">The scheduleType.</param>
        /// <returns>Week.</returns>
        public static Week GetWeekValue(this ResolutionContext context, string scheduleType)
        {
            if (context.Items.TryGetValue(WeekKey, out var week))
            {
                return (Week)week;
            }

            throw new InvalidOperationException($"Week for {scheduleType} not set.");
        }

        /// <summary>
        /// Set week value.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="week">The week.</param>
        public static void SetWeekValue(this IMappingOperationOptions options, Week week)
        {
            options.Items[WeekKey] = week;
        }
    }
}
