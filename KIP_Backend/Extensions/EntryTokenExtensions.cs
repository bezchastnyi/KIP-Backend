using KIP_Backend.Secrets;
using Microsoft.AspNetCore.Builder;

namespace KIP_Backend.Extensions
{
    /// <summary>
    /// Tokens extensions.
    /// </summary>
    public static class EntryTokenExtensions
    {
        /// <summary>
        /// Using tokens.
        /// </summary>
        /// <returns>Pattern.</returns>
        /// <param name="builder">The builder.</param>
        /// <param name="pattern">The pattern.</param>
        public static IApplicationBuilder UseTokens(this IApplicationBuilder builder, string pattern)
        {
            return builder.UseMiddleware<EntryToken>(pattern);
        }
    }
}
