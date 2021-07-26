// <copyright file="KIPFacultiesShortNames.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace KIP_server_NoAuth.Constants
{
    /// <summary>
    /// KIP short names for faculties constants.
    /// </summary>
    public class KIPFacultiesShortNames
    {
        /// <summary>
        /// Gets short names of faculties.
        /// </summary>
        public static Dictionary<string, string> FacultiesShortNames { get; } = new Dictionary<string, string>
        {
            { "Соціально-гуманітарних технологій", "СГТ" },
            { "Комп`ютерних наук і програмної інженерії", "КН" },
            { "Навчально-науковий інститут енергетики, електроніки та електромеханіки", "ІЕЕЕ" },
            { "Навчально-науковий інститут механічної інженерії і транспорту", "МІТ" },
            { "Навчально-науковий інженерно-фізичний інститут", "Інфіз" },
            { "Навчально-науковий інститут хімічних технологій та інженерії", "ХТ" },
            { "Навчально-науковий інститут економіки, менеджменту і міжнародного бізнесу", "БЕМ" },
            { "Комп`ютерні та інформаційні технології", "КІТ" },
        };
    }
}
