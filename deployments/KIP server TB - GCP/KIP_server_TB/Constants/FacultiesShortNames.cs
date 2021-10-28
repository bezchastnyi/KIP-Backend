using System.Collections.Generic;

namespace KIP_Backend.Constants
{
    /// <summary>
    /// Short names for faculties constants.
    /// </summary>
    public static class FacultiesShortNames
    {
        /// <summary>
        /// Gets short names of faculties.
        /// </summary>
        public static Dictionary<string, string> ShortNames { get; } = new Dictionary<string, string>
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
