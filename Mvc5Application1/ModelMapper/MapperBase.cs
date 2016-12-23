using Mvc5Application1.Business.Contracts.RefData;
using Mvc5Application1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Mvc5Application1.ModelMapper
{
    public abstract class MapperBase<TEntity, TViewModel> : IMapper<TEntity, TViewModel>
    {
        protected readonly IRefDataBusiness RefData;
        public IMapperFactory MapperFactory { get; set; }

        protected MapperBase(IRefDataBusiness refData)
        {
            RefData = refData;
        }

        public virtual TEntity BuildEntity(TViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public virtual TViewModel BuildViewModel(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual TViewModel BuildViewModel(TEntity entity, TViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public virtual TViewModel RebuildViewModel(TViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        protected virtual void GetCheckBoxLists(TViewModel viewModel)
        {
        }

        protected virtual void GetSelectLists(TViewModel viewModel)
        {
        }

        public virtual TViewModel BuildEmptyViewModel()
        {
            var viewModel = (TViewModel)Activator.CreateInstance(typeof(TViewModel));
            GetCheckBoxLists(viewModel);
            GetSelectLists(viewModel);
            return viewModel;
        }

        protected IEnumerable<SelectListItem> RefDataToSelectListItems<T>(Func<T, SelectListItem> refDataToListItemMapper) where T : class
        {
            var refDatas = RefData.GetRefData<T>();
            return refDatas.Select(refDataToListItemMapper).OrderBy(x => x.Text).ToList();
        }

        protected IEnumerable<CheckboxListItem> RefDataToCheckBoxListItems<T>(Func<T, CheckboxListItem> refDataToListItemMapper) where T : class
        {
            var refDatas = RefData.GetRefData<T>();
            return refDatas.Select(refDataToListItemMapper).OrderBy(x => x.Text).ToList();
        }
    }
}