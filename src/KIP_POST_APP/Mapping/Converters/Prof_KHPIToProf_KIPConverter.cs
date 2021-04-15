// <copyright file="Prof_KHPIToProf_KIPConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using AutoMapper;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;

namespace KIP_POST_APP.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP teachers model from the KhPI teachers.
    /// </summary>
    public class Prof_KHPIToProf_KIPConverter : ITypeConverter<Prof_KHPI, Prof>
    {
        /// <summary>
        /// Convert teachers names.
        /// </summary>
        /// <returns>
        /// Name of teachers.
        /// </returns>
        /// <param name="source">Building KHPI.</param>
        /// <param name = "destination">A g</param>
        /// <param name= "context">A </param>
        public Prof Convert(Prof_KHPI source, Prof destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var fio = this.SearchFIO(source.title);

            var profSurname = " ";
            var profName = " ";
            var profPatronymic = " ";

            if (fio.Count > 0)
            {
                profSurname = fio[0];
            }

            if (fio.Count > 1)
            {
                profName = fio[1];
            }

            if (fio.Count > 2)
            {
                profPatronymic = fio[2];
            }

            var obj = new Prof
            {
                ProfID = source.id,
                ProfSurname = profSurname,
                ProfName = profName,
                ProfPatronymic = profPatronymic,
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
