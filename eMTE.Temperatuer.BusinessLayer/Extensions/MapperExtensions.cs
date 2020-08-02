using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace eMTE.Temperature.BusinessLayer.Extensions
{
    public static class MapperExtensions
    {
        static public IMapper Mapper { get; set; }
        public static T To<T>(this object soure)
        {
            return Mapper.Map<T>(soure);
        }

        public static T2 Into<T1, T2>(this T1 soure, T2 destination)
            where T1 : class
            where T2 : class
        {
            return Mapper.Map(soure, destination);
        }

        public static List<T> ToList<T>(this IEnumerable<object> source)
        {
            return source.Select(s => s.To<T>()).ToList();
        }
    }
}
