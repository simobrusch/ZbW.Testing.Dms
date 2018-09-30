using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
    public class DocumentService
    {
        internal Serialize SerializeTestable { get; set; }
        private string _repositoryDir;

        public DocumentService()
        {
            SerializeTestable = new Serialize();
        }

        #region Methods
        public string GetRepositoryDir()
        {
            _repositoryDir = ConfigurationManager.AppSettings.Get("RepositoryDir").ToString();
            return _repositoryDir;
        }

        public string GetContentFileName(Guid documentId, string extension)
        {
            var filename = string.Concat(documentId, "_Content", extension);
            return filename;
        }

        public string GetMetadataFileName(Guid documentId)
        {
            var mdfilename = string.Concat(documentId, "_Metadata.xml");
            return mdfilename;
        }

        public string SerializeMetadataItem(Serialize serializeTestable, MetadataItem metadataItem)
        {
            var result = serializeTestable.SerializeMetadataItem(serializeTestable, metadataItem);
            return result;
        }

        public MetadataItem DeserializeMetadataItem(Serialize serializeTestable, string path)
        {
            var result = serializeTestable.DeserializeMetadataItem(path);
            return result;
        }

        public void OpenFile(Serialize serializeTestable, MetadataItem metadataItem)
        {
            if (serializeTestable == null)
            {
                throw new ArgumentNullException(nameof(serializeTestable));
            }

            var process = Process.Start(metadataItem.FilePath);
        }
        #endregion
    }
}
