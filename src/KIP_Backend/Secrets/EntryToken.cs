using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace KIP_Backend.Secrets
{
    /// <summary>
    /// Entry token.
    /// </summary>
    public class EntryToken
    {
        private readonly ILogger<EntryToken> _logger;
        private readonly RequestDelegate _next;
        private readonly string pattern;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryToken"/> class.
        /// </summary>
        /// <param name="next">Next request.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="logger">The logger.</param>
        public EntryToken(RequestDelegate next, string pattern, ILogger<EntryToken> logger)
        {
            this._next = next;
            this.pattern = pattern;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Call asynchronous mode.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (token != this.pattern || string.IsNullOrWhiteSpace(token))
            {
                this._logger.Log(LogLevel.Error, "CREDENTIALS ERROR -> Entry Token is invalid");
                context.Response.StatusCode = 403;
            }
            else
            {
                this._logger.Log(LogLevel.Information, "MESSAGE -> Authentication is SUCCESSFUL");
                await this._next.Invoke(context);
            }
        }
    }
}
