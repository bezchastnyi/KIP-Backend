using AutoMapper;
using System;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;
using System.Text.RegularExpressions;

namespace KIP_POST_APP.Mapping.Converters
{
    public class Audience_KHPIToAudience_KIPConverter : ITypeConverter<Audience_KHPI, Audience>
    {
        public Audience Convert(Audience_KHPI source, Audience destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var obj = new Audience
            {
                AudienceID = source.id,
                AudienceName = FixTitle(source.title),
                NumberOfSeats = SearchNumberOfSeats(source.title)
            };

            return obj;
        }

        private string FixTitle(string title)
        {
            foreach (var part in title.Split('['))
            {
                return part;
            }

            return "";
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
