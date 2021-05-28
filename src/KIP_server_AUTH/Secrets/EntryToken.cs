using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace KIP_server_AUTH.Secrets
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
            this._next = next ?? throw new ArgumentNullException(nameof(next));
            this.pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
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
                var message = "CREDENTIALS ERROR -> Entry Token is invalid";
                this._logger.Log(LogLevel.Error, message);

                context.Response.StatusCode = 403;
            }
            else
            {
                var message = "MESSAGE -> Authentication is SUCCESSFUL";
                this._logger.Log(LogLevel.Information, message);

                await this._next.Invoke(context);
            }
        }
    }
}
