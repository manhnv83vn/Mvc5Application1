using Mvc5Application1.Data.Model;
using System.Collections.Generic;

namespace Mvc5Application1.Business.Contracts
{
    public interface ISecurityMatrixRepository
    {
        List<vwSecutiryMatrix> GetSecurityMatrix();
    }
}