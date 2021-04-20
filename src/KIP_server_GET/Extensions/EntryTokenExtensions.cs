using KIP_server_GET.Secrets;
using Microsoft.AspNetCore.Builder;

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
