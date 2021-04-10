using AutoMapper;
using System;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;
using KIP_POST_APP.Constants;

namespace KIP_POST_APP.Mapping.Converters
{
    public class Faculty_KHPIToFaculty_KIPConverter : ITypeConverter<Faculty_KHPI, Faculty>
    {
        public Faculty Convert(Faculty_KHPI source, Faculty destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var shortName = string.Empty;
            foreach(var faculty in KIPFacultiesShortNames.facultiesShortNames)
            {
                if (faculty.Key == source.title)
                    shortName = faculty.Value;
            }

            var obj = new Faculty
            {
                FacultyID = source.id,
                FacultyName = source.title,
                FacultyShortName = shortName
            };

            return obj;
        }
    }
}
