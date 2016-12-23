using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Mvc5Application1.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum enumerationValue)
        {
            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = (enumerationValue.GetType()).GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }

        public static Dictionary<int, string> ToDictionary(this Type enumType)
        {
            return Enum.GetValues(enumType)
                .Cast<object>()
                .ToDictionary(k => (int)k, v => ((Enum)v).GetDescription());
        }
    }
}