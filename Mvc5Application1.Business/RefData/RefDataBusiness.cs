using Mvc5Application1.Business.Contracts.RefData;
using Mvc5Application1.Data.Contracts;
using Mvc5Application1.Data.Model;
using Mvc5Application1.Framework.Caching;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mvc5Application1.Business.RefData
{
    /// <summary>
    /// Class RefDataBusiness
    /// </summary>
    public class RefDataBusiness : IRefDataBusiness
    {
        private readonly ICacheManager _cacheManager;

        private readonly IRefDataRepository _refDataRepository;

        public RefDataBusiness(ICacheManager cacheManager, IRefDataRepository refDataRepository)
        {
            _cacheManager = cacheManager;
            _refDataRepository = refDataRepository;
        }

        public List<T> GetRefData<T>() where T : class
        {
            string key = typeof(T).FullName;
            List<T> refData;
            if (_cacheManager.IsSet(key))
            {
                refData = (List<T>)_cacheManager.Get(key);
            }
            else
            {
                refData = _refDataRepository.Get<T>();
                _cacheManager.Set(key, refData, 120);
            }
            return refData;
        }

        public T GetRefDataItem<T>(Func<T, bool> predicate) where T : class
        {
            var item = GetRefData<T>().Where(predicate).FirstOrDefault();
            return item;
        }

        public bool ExistRefDataItem<T>(Predicate<T> predicate) where T : class
        {
            var exist = GetRefData<T>().Exists(predicate);
            return exist;
        }

        public List<PaymentType> GetPaymentTypes()
        {
            return GetRefData<PaymentType>();
        }
    }
}