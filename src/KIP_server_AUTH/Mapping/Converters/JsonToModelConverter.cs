// <copyright file="JsonToModelConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace KIP_server_AUTH.Mapping.Converters
{
    /// <summary>
    /// Convert json format of data to model.
    /// </summary>
    public static class JsonToModelConverter
    {
        /// <summary>
        /// Getting data from json.
        /// </summary>
        /// <returns>
        /// Data from json.
        /// </returns>
        /// <param name="url">Link to json.</param>
        /// <typeparam name="T">Model type.</typeparam>
        public static IEnumerable<T> GetJsonData<T>(string url)
        {
            using (var web = new WebClient())
            {
                var jsonData = string.Empty;

                try
                {
                    jsonData = web.DownloadString(url);

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
