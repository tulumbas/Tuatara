using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tuatara.Data.Xml
{
    public static class XmlContextExtensions
    {
        public static string XMLDATA_FOLDER;

        public static int GetIntFromChild(this XElement element, string childNodeName)
        {
            var val = GetStringFromChild(element, childNodeName);
            return int.Parse(val);
        }

        public static int? GetNullableIntFromChild(this XElement element, string childNodeName)
        {
            var val = GetStringFromChild(element, childNodeName);
            return string.IsNullOrEmpty(val) ? null : (int?)int.Parse(val);
        }

        public static DateTime? GetStampFromChild(this XElement element, string childNodeName)
        {
            var val = GetStringFromChild(element, childNodeName);
            DateTime dt;
            if (!string.IsNullOrEmpty(val) && DateTime.TryParse(val, out dt))
            {
                return dt;
            }
            return null;
        }

        public static double? GetDoubleFromChild(this XElement element, string childNodeName)
        {
            var val = GetStringFromChild(element, childNodeName);
            return string.IsNullOrEmpty(val) ? null : (double?)double.Parse(val);
        }

        public static string GetStringFromChild(this XElement element, string childNodeName)
        {
            var val = element.Element(childNodeName)?.Value;
            if (val == null)
            {
                val = element.Attribute(childNodeName)?.Value;
            }
            return val;
        }

    }
}
