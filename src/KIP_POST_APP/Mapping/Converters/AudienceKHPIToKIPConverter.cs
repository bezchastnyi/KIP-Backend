// <copyright file="AudienceKHPIToKIPConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Text.RegularExpressions;
using AutoMapper;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;

namespace KIP_POST_APP.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP audience model from the KhPI audience.
    /// </summary>
    public class AudienceKHPIToKIPConverter : ITypeConverter<AudienceKHPI, Audience>
    {
        /// <summary>
        /// Convert model of audience from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of audience of model audience KIP.
        /// </returns>
        /// <param name="source">Model of audience KHPI.</param>
        /// <param name = "destination">Model of audience KIP.</param>
        /// <param name= "context">The context. </param>
        public Audience Convert(AudienceKHPI source, Audience destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(source.title))
            {
                return null;
            }

            return new Audience
            {
                AudienceID = source.id,
                AudienceName = this.Fixtitle(source.title),
                NumberOfSeats = this.SearchNumberOfSeats(source.title),
            };
        }

        private string Fixtitle(string title)
        {
            foreach (var part in title.Split('['))
            {
                return part;
            }

            return title;
        }

        private int SearchNumberOfSeats(string title)
        {
            var regex = new Regex(@"[\d[0-9]{0,4} місць]");
            var matches = regex.Matches(title);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    regex = new Regex(@"\d[0-9]{0,4}");
                    var matches2 = regex.Matches(match.Value);
                    foreach (Match match2 in matches2)
                    {
                        if (matches2.Count > 0)
                        {
                            return int.Parse(match2.Value);
                        }
                    }
                }
            }

            return 0;
        }
    }
}
