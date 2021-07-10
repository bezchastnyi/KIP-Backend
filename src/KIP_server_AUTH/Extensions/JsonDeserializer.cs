// <copyright file="JsonDeserializer.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KIP_server_AUTH.Extensions
{
    /// <summary>
    /// Convert json format of data to model.
    /// </summary>
    public static class JsonDeserializer
    {
        /// <summary>
        /// Json Deserializer.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="url">The json url.</param>
        /// <returns>Built model from json.</returns>
        public static async Task<IEnumerable<T>> ExecuteAsync<T>(string url)
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + ": " + e.StackTrace + "\n URL = " + url);
                }

                return default;
            }
        }
    }
}
