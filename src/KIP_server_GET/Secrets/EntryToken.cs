using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace KIP_server_GET.Secrets
{
    public class EntryToken
    {
        private readonly ILogger<EntryToken> _logger;
        private readonly RequestDelegate _next;
        private readonly string pattern;

        public EntryToken(RequestDelegate next, string pattern, ILogger<EntryToken> logger)
        {
            this._next = next;
            this.pattern = pattern;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (token != pattern || string.IsNullOrWhiteSpace(token))
            {
                var message = "CREDENTIALS ERROR -> Entry Token is invalid";
                _logger.Log(LogLevel.Error, message);

                context.Response.StatusCode = 403;
            }
            else
            {
                var message = "MESSAGE -> Authentication is SUCCESSFUL";
                _logger.Log(LogLevel.Information, message);

                await _next.Invoke(context);
            }
        }
    }
}
