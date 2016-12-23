using Mvc5Application1.Data.Contracts;
using Mvc5Application1.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace Mvc5Application1.Data.Repository
{
    public class RefDataRepository : IRefDataRepository
    {
        private readonly TestDbContext _dbContext;

        public RefDataRepository()
        {
            _dbContext = new TestDbContext();
        }

        public List<T> Get<T>() where T : class
        {
            var refData = _dbContext.Set<T>().ToList();
            return refData;
        }

        public List<PaymentType> GetPaymentTypes()
        {
            return (from p in _dbContext.PaymentType select p).ToList();
        }
    }
}