// <copyright file="JsonDeserializeService.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

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
            using (var web = new WebClient())
            {
                var jsonData = string.Empty;

                try
                {
                    jsonData = await web.DownloadStringTaskAsync(url);

                    if (jsonData.Contains("<!DOCTYPE html>"))
                    {
                        return default;
                    }

                    return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonData);
                }
                catch (Exception ex)
                {
                    this._logger.LogJsonDeserializeUnexpectedError(url, ex);
                    return default;
                }
            }
        }
    }
}
