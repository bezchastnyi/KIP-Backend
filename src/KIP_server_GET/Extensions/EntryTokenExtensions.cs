using KIP_server_GET.Secrets;
using Microsoft.AspNetCore.Builder;

public static class TokenExtensions
{
    public static IApplicationBuilder UseTokens(this IApplicationBuilder builder, string pattern)
    {
        return builder.UseMiddleware<EntryToken>(pattern);
    }
}