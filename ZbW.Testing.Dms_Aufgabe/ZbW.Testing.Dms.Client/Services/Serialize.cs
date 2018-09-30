using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
    public class Serialize
    {
        public virtual string SerializeMetadataItem(Serialize serializeTestable, MetadataItem metadataItem)
        {
            var xmlserializer = new XmlSerializer(typeof(MetadataItem));
            var stringWriter = new StringWriter();
            var writer = XmlWriter.Create(stringWriter);

            xmlserializer.Serialize(writer, metadataItem);

            var serializeXml = stringWriter.ToString();

            writer.Close();

            return serializeXml;
        }

        public virtual MetadataItem DeserializeMetadataItem(string path)
        {
            var serializer = new XmlSerializer(typeof(MetadataItem));

            var reader = new StreamReader(path);
            var metadataItem = (MetadataItem)serializer.Deserialize(reader);
            reader.Close();

            return metadataItem;
        }
    }
}
