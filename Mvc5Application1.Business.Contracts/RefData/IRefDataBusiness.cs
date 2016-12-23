using Mvc5Application1.Data.Model;
using System;
using System.Collections.Generic;

namespace Mvc5Application1.Business.Contracts.RefData
{
    public interface IRefDataBusiness
    {
        List<T> GetRefData<T>() where T : class;

        T GetRefDataItem<T>(Func<T, bool> predicate) where T : class;

        bool ExistRefDataItem<T>(Predicate<T> predicate) where T : class;

        List<PaymentType> GetPaymentTypes();
    }
}