using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using KIP_server_Auth.Extensions;
using KIP_server_Auth.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace KIP_server_Auth.Services
{
    /// <summary>
    /// Convert json format of data to model.
    /// </summary>
    public class JsonDeserializeService : IDeserializeService
    {
        private readonly ILogger<JsonDeserializeService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDeserializeService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public JsonDeserializeService(ILogger<JsonDeserializeService> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> ExecuteAsync<T>(string url)
        {
            using var web = new WebClient();
            try
            {
                var jsonData = await web.DownloadStringTaskAsync(url);
                return jsonData.Contains("<!DOCTYPE html>") ? default : JsonConvert.DeserializeObject<IEnumerable<T>>(jsonData);
            }
            catch (Exception ex)
            {
                this._logger.LogJsonDeserializeUnexpectedError(url, ex);
                return default;
            }
        }
    }
}
