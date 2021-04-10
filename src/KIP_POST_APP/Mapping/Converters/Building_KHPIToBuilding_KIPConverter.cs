using AutoMapper;
using System;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;
using KIP_POST_APP.Constants;

namespace KIP_POST_APP.Mapping.Converters
{
    public class Building_KHPIToBuilding_KIPConverter : ITypeConverter<Building_KHPI, Building>
    {
        public Building Convert(Building_KHPI source, Building destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var shortName = string.Empty;
            foreach (var faculty in KIPBuildingShortNames.buildingShortNames)
            {
                if (faculty.Key == source.title)
                    shortName = faculty.Value;
            }

            var obj = new Building
            {
                BuildingID = source.id,
                BuildingName = source.title,
                BuildingShortName = shortName
            };

            return obj;
        }
    }
}
