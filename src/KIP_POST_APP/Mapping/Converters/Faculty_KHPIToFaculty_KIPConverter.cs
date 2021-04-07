using AutoMapper;
using System;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;

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

            var obj = new Faculty
            {
                FacultyID = source.id,
                FacultyName = source.title
            };

            return obj;
        }
    }
}
