using Mvc5Application1.Business.Contracts.RefData;
using System;
using System.Collections.Generic;

namespace Mvc5Application1.ModelMapper
{
    public class MapperFactory : IMapperFactory
    {
        private readonly IRefDataBusiness refData;
        private readonly IDictionary<Tuple, object> dictionary;

        public MapperFactory(IRefDataBusiness refData)
        {
            this.refData = refData;
            dictionary = new Dictionary<Tuple, object>();
        }

        public IMapper<TEntity, TViewModel> CreateMap<TEntity, TViewModel>()
        {
            return (IMapper<TEntity, TViewModel>)dictionary[new Tuple(typeof(TEntity), typeof(TViewModel))];
        }

        public void RegisterMap<TEntity, TViewModel>(IMapper<TEntity, TViewModel> mapper)
        {
            RegisterMap(mapper, typeof(TEntity), typeof(TViewModel));
        }

        public void RegisterMap(dynamic mapper, Type tEntity, Type tViewModel)
        {
            mapper.MapperFactory = this;
            dictionary[new Tuple(tEntity, tViewModel)] = mapper;
        }

        private class Tuple
        {
            private readonly Type entity;
            private readonly Type viewModel;

            public Tuple(Type entity, Type viewModel)
            {
                this.entity = entity;
                this.viewModel = viewModel;
            }

            private bool Equals(Tuple other)
            {
                return Equals(entity, other.entity) && Equals(viewModel, other.viewModel);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((entity != null ? entity.GetHashCode() : 0) * 397) ^ (viewModel != null ? viewModel.GetHashCode() : 0);
                }
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((Tuple)obj);
            }
        }
    }
}