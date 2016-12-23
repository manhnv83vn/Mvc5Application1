namespace Mvc5Application1.ModelMapper
{
    public interface IMapper<TEntity, TViewModel>
    {
        TEntity BuildEntity(TViewModel viewModel);

        TViewModel BuildViewModel(TEntity entity);

        TViewModel BuildViewModel(TEntity entity, TViewModel viewModel);

        TViewModel RebuildViewModel(TViewModel viewModel);

        TViewModel BuildEmptyViewModel();

        IMapperFactory MapperFactory { get; set; }
    }
}