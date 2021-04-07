using KIP_POST_APP.Secrets;
using Microsoft.AspNetCore.Builder;

public static class TokenExtensions
{
    public static IApplicationBuilder UseTokens(this IApplicationBuilder builder, string pattern)
    {
        return builder.UseMiddleware<EntryToken>(pattern);
    }
}