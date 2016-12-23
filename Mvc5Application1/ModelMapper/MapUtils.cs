using AutoMapper;
using System.Linq;

namespace Mvc5Application1.ModelMapper
{
    public class MapUtils
    {
        public void CreateMap<TSource, TDestination>(IMappingExpression<TSource, TDestination> mappingExpression)
        {
        }

        private IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType == sourceType
                && x.DestinationType == destinationType);
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }
    }

    public class MyMapper<T1, T2>
    {
        private IMappingExpression<T1, T2> _mapping12;
        private IMappingExpression<T2, T1> _mapping21;

        public MyMapper()
        {
            _mapping12 = IgnoreAllNonExisting(Mapper.CreateMap<T1, T2>());
            _mapping21 = IgnoreAllNonExisting(Mapper.CreateMap<T2, T1>());
        }

        private IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType == sourceType
                && x.DestinationType == destinationType);
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }
    }
}