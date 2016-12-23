using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Mvc5Application1.Framework
{
    public static class XmlSerializeHelper
    {
        public static string Serialize(Object obj)
        {
            var serializer = new XmlSerializer(obj.GetType());
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { OmitXmlDeclaration = true });
            serializer.Serialize(xmlWriter, obj, ns);

            return stringWriter.ToString();
        }
    }
}