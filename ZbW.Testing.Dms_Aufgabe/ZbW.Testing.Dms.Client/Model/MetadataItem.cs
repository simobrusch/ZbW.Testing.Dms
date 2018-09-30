using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.Model
{
    public class MetadataItem
    {
        #region variables
        public string FilePath { get; set; }
        public string Bezeichnung { get; set; }
        public string Tag { get; set; }
        public DateTime ErstellungsDatum { get; set; }
        public DateTime ValutaDatum { get; set; }
        public string Typ { get; set; }
        public string Benutzer { get; set; }
        public string OriginalPath { get; set; }
        public bool IsRemoveFileEnabled { get; set; }
        public string ContentFileExtension { get; set; }
        public string ContentFileName { get; set; }
        public string MetadataFileName { get; set; }
        public Guid DocumentId { get; set; }
        public string RepoYear { get; set; }

        private readonly DocumentService _documentService;

        #endregion

        public MetadataItem()
        {
            _documentService = new DocumentService();
        }

        #region Methods
        private void LoadMetadata(IEnumerable<KeyValuePair<string, List<MetadataItem>>> yearItems)
        {
            foreach (var yearItem in yearItems)
            {
                var metadataFiles = Directory.GetFiles(yearItem.Key, "*_Metadata.xml");

                foreach (var metadataFile in metadataFiles)
                {
                    var xmlSerializer = new XmlSerializer(typeof(MetadataItem));
                    var streamReader = new StreamReader(metadataFile);
                    var metadataItem = (MetadataItem)xmlSerializer.Deserialize(streamReader);

                    yearItem.Value.Add(metadataItem);
                }
            }
        }

        private List<KeyValuePair<string, List<MetadataItem>>> GetYearItems()
        {
            var yearItems = new List<KeyValuePair<string, List<MetadataItem>>>();
            return yearItems;
        }


        public void AddFile(MetadataItem metadataItem, bool deleteFile)
        {
            var repositoryDir = _documentService.GetRepositoryDir();
            var year = metadataItem.ValutaDatum.Year;
            var documentId = Guid.NewGuid();
            var extension = Path.GetExtension(metadataItem.OriginalPath);
            var contentFileName = _documentService.GetContentFileName(documentId, extension);
            var metadataFileName = _documentService.GetMetadataFileName(documentId);

            var targetDir = Path.Combine(repositoryDir, year.ToString());


            metadataItem.ContentFileExtension = extension;
            metadataItem.ContentFileName = contentFileName;
            metadataItem.MetadataFileName = metadataFileName;
            metadataItem.DocumentId = documentId;
            metadataItem.RepoYear = year.ToString();
            metadataItem.FilePath = Path.Combine(targetDir, contentFileName);


            var xmlSerializer = new XmlSerializer(typeof(MetadataItem));

            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            var streamWriter = new StreamWriter(Path.Combine(targetDir, metadataFileName));
            xmlSerializer.Serialize(streamWriter, metadataItem);
            streamWriter.Flush();
            streamWriter.Close();


            File.Copy(metadataItem.OriginalPath, Path.Combine(targetDir, contentFileName));

            if (deleteFile)
            {
                var task = Task.Factory.StartNew(
                    () =>
                    {
                        Task.Delay(500);
                        File.Delete(metadataItem.OriginalPath);
                    });
                try
                {
                    Task.WaitAll(task);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        #endregion
    }
}