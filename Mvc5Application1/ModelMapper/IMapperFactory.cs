using System;

namespace Mvc5Application1.ModelMapper
{
    public interface IMapperFactory
    {
        IMapper<TEntity, TViewModel> CreateMap<TEntity, TViewModel>();

        void RegisterMap<TEntity, TViewModel>(IMapper<TEntity, TViewModel> mapper);

        void RegisterMap(dynamic mapper, Type tEntity, Type tViewModel);
    }
}