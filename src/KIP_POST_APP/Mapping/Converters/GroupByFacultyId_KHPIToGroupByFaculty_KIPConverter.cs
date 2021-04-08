using AutoMapper;
using System;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;
using KIP_POST_APP.Services;

namespace KIP_POST_APP.Mapping.Converters
{
    public class GroupByFacultyId_KHPIToGroupByFaculty_KIPConverter : ITypeConverter<Group_KHPI, Group>
    {
        public Group Convert(Group_KHPI source, Group destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var obj = new Group
            {
                GroupID = source.id,
                GroupName = source.title,
                Course = source.course
            };

            return obj;
        }
    }
}
