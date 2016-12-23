using Mvc5Application1.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mvc5Application1.Helpers
{
    public static class FormatHelper
    {
        public static string QuantityDisplayFormat(decimal? quantity, List<string> uoMListItems, string uomValue)
        {
            if (uoMListItems.FirstOrDefault(x => x == "LM") == uomValue)
                return quantity != null ? Convert.ToDecimal(quantity).ToString("N3") : null;
            if (uoMListItems.FirstOrDefault(x => x == "EA") == uomValue)
                return quantity != null ? Convert.ToDecimal(quantity).ToString("N0") : null;

            return quantity != null ? Convert.ToDecimal(quantity).ToString("N2") : null;
        }

        public static string SetFormatIntToThreeChar(string str)
        {
            if (str.Length > 3) return str;

            switch (str.Length)
            {
                case 1:
                    return "00" + str;

                case 2:
                    return "0" + str;

                default:
                    return string.Empty;
            }
        }

        public static string GetPipingComponentDescription(string pipingClass, string degree, string elbowType,
            string radius, string orientation, string shape, string pipingComponent, int? nbSize1, int? nbSize2)
        {
            return pipingComponent
                + (!string.IsNullOrEmpty(pipingClass) ? ", " + pipingClass : string.Empty)
                + (!string.IsNullOrEmpty(degree) ? ", " + degree + " Deg" : string.Empty)
                + (!string.IsNullOrEmpty(elbowType) ? ", " + elbowType : string.Empty)
                + (!string.IsNullOrEmpty(radius) ? ", " + radius : string.Empty)
                + (!string.IsNullOrEmpty(orientation) ? ", " + orientation : string.Empty)
                + (!string.IsNullOrEmpty(shape) ? ", " + shape : string.Empty)
                + (nbSize1 != null ? ", " + nbSize1.Value + " mm" : string.Empty)
                + (nbSize2 != null ? ", " + nbSize2.Value + " mm" : string.Empty);
        }

        public static string GetFormatedDate(DateTime? date)
        {
            return date == null ? string.Empty : ((DateTime)date).ToString(CommonString.DateFormat);
        }
    }
}