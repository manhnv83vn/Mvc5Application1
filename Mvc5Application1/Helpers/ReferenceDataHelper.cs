using Microsoft.Practices.ServiceLocation;
using Mvc5Application1.Business.Contracts.RefData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Mvc5Application1.Helpers
{
    public static class ReferenceDataHelper
    {
        private static Dictionary<Type, KeyValuePair<MethodInfo, MethodInfo>> _getters = new Dictionary<Type, KeyValuePair<MethodInfo, MethodInfo>>();

        public static SelectListItem ToSelectListItem(object item, MethodInfo keyGetter, MethodInfo valueGetter)
        {
            return new SelectListItem
            {
                Value = keyGetter.Invoke(item, null).ToString(),
                Text = valueGetter.Invoke(item, null).ToString()
            };
        }

        public static void RetrieveGetters(Type objectType, out MethodInfo keyGetter, out MethodInfo valueGetter)
        {
            KeyValuePair<MethodInfo, MethodInfo> getters;
            if (!_getters.TryGetValue(objectType, out getters))
            {
                var idProperty = objectType.GetProperty(objectType.Name + "Id");
                if (idProperty == null)
                {
                    throw new EntryPointNotFoundException(String.Format("Could not find '{0}Id' property",
                        objectType.Name));
                }

                var nameProperty = objectType.GetProperty("Name");
                if (nameProperty == null)
                {
                    throw new EntryPointNotFoundException("Could not find 'Name' property");
                }
                getters = new KeyValuePair<MethodInfo, MethodInfo>(idProperty.GetGetMethod(), nameProperty.GetGetMethod());
                _getters[objectType] = getters;
            }
            keyGetter = getters.Key;
            valueGetter = getters.Value;
        }

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(Func<IRefDataBusiness, IEnumerable<T>> method, Func<T, string> textSelector, Func<T, string> valueSelector)
        {
            var items = method(ServiceLocator.Current.GetInstance<IRefDataBusiness>());

            return items.Select(item => new SelectListItem
            {
                Text = textSelector(item),
                Value = valueSelector(item)
            });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(Func<IRefDataBusiness, IEnumerable<T>> method)
        {
            var items = method(ServiceLocator.Current.GetInstance<IRefDataBusiness>());
            MethodInfo keyGetter = null;
            MethodInfo valueGetter = null;
            foreach (var item in items)
            {
                if (keyGetter == null)
                {
                    RetrieveGetters(typeof(T), out keyGetter, out valueGetter);
                }
                yield return ToSelectListItem(item, keyGetter, valueGetter);
            }
        }
    }
}