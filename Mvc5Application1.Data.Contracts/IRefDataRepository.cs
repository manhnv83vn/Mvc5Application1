using Mvc5Application1.Data.Model;
using System.Collections.Generic;

namespace Mvc5Application1.Data.Contracts
{
    public interface IRefDataRepository
    {
        List<T> Get<T>() where T : class;

        List<PaymentType> GetPaymentTypes();
    }
}