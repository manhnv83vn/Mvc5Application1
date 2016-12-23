using Mvc5Application1.Data.Model;
using System.Collections.Generic;

namespace Mvc5Application1.Business.Security
{
    public class SecurityMatrixDict
    {
        private readonly IDictionary<string, bool> _securityDictionary;

        public SecurityMatrixDict(List<vwSecutiryMatrix> securityMappingList)
        {
            _securityDictionary = new Dictionary<string, bool>();
            foreach (var securityMappingLine in securityMappingList)
            {
                _securityDictionary.Add(securityMappingLine.FunctionName, true);
            }
        }

        public bool GetValue(string function)
        {
            if (_securityDictionary.ContainsKey(function))
            {
                return _securityDictionary[function];
            }
            return false;
        }

        private string GetKey(string resource, string operation, string role)
        {
            return string.Format("{0}/{1}/{2}", resource.ToLower(), operation.ToLower(), role.ToLower());
        }
    }
}