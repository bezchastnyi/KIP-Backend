using AutoMapper;
using System;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;

namespace KIP_POST_APP.Mapping.Converters
{
    public class Cathedra_KHPIToCathedra_KIPConverter : ITypeConverter<Cathedra_KHPI, Cathedra>
    {
        public Cathedra Convert(Cathedra_KHPI source, Cathedra destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var obj = new Cathedra
            {
                CathedraID = source.id,
                CathedraName = source.title
            };

            return obj;
        }
    }
}
