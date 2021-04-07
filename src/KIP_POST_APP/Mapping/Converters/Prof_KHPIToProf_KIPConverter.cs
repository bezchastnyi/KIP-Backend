using AutoMapper;
using System;
using KIP_POST_APP.Models.KHPIDB;
using KIP_POST_APP.Models.KIPDB;
using System.Collections.Generic;

namespace KIP_POST_APP.Mapping.Converters
{
    public class Prof_KHPIToProf_KIPConverter : ITypeConverter<Prof_KHPI, Prof>
    {
        public Prof Convert(Prof_KHPI source, Prof destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var FIO = SearchFIO(source.title);

            var profSurname = "";
            var profName = "";
            var profPatronymic = "";

            if (FIO.Count > 0)
            {
                profSurname = FIO[0];
            }

            if (FIO.Count > 1)
            {
                profName = FIO[1];
            }

            if (FIO.Count > 2)
            {
                profPatronymic = FIO[2];
            }

            var obj = new Prof
            {
                ProfID = source.id,
                ProfSurname = profSurname,
                ProfName = profName,
                ProfPatronymic = profPatronymic
            };
            return obj;
        }

        private List<string> SearchFIO(string title)
        {
            var list = new List<string>();
            foreach (var part in title.Split(' '))
            {
                list.Add(part);
            }
            return list;
        }
    }
}
