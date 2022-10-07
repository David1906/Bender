using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bender.Helpers
{
    public class MapperHelper
    {
        private Mapper Mapper { get; set; }

        public MapperHelper(MapperConfiguration configuration)
        {
            this.Mapper = new Mapper(configuration);
        }

        public static MapperHelper Create<TSource, TDestination>()
        {
            return new MapperHelper(new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()));
        }

        public T Map<T>(object source)
        {
            return this.Mapper.Map<T>(source);
        }
    }
}
