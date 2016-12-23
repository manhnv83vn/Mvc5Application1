using Mvc5Application1.Business.Contracts;
using Mvc5Application1.Data.Model;
using Mvc5Application1.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Mvc5Application1.Data.Repository
{
    public class SecurityMatrixRepository : Repository<vwSecutiryMatrix>, ISecurityMatrixRepository
    {
        public SecurityMatrixRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<vwSecutiryMatrix> GetSecurityMatrix()
        {
            var principal = Thread.CurrentPrincipal as Mvc5Application1Principal;
            if (principal == null)
            {
                throw new ApplicationException("Your session has timeout. Please login again.");
            }

            var sercurityMappingLines = GetAll().Where(x => x.UserName == principal.Identity.Name).ToList();
            return sercurityMappingLines;
        }
    }
}