using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
    public class SearchDocument
    {
        private readonly DocumentService _documentService;

        private readonly string _targetPath;

        public SearchDocument()
        {
            _documentService = new DocumentService();
            _targetPath = _documentService.GetRepositoryDir();
        }

        #region Properties
        public List<MetadataItem> MetadataItems { get; set; }

        #endregion

        #region Methods
        public List<MetadataItem> FilterMetadataItems(string type, string searchParam)
        {
            if (string.IsNullOrEmpty(searchParam) && string.IsNullOrEmpty(type))
            {
                return this.MetadataItems;
            }

            if (string.IsNullOrEmpty(searchParam))
            {
                searchParam = "";
            }

            var filteredItems = this.MetadataItems.Where(item => (item.Bezeichnung.Contains(searchParam) ||
                                                                  item.Tag.Contains(searchParam) ||
                                                                  item.ErstellungsDatum.ToString().Contains(searchParam) ||
                                                                  item.ValutaDatum.ToString().Contains(searchParam)) &&
                                                                 (string.IsNullOrEmpty(type) || item.Typ.Equals(type))).ToList();

            return filteredItems;
        }

        public List<MetadataItem> GetAllMetadataItems()
        {
            var folderPaths = GetAllFolderPaths(this._targetPath);
            var xmlPathsFromAllFolders = new ArrayList();
            var metadataItemList = new ArrayList();

            foreach (var folderPath in folderPaths)
            {
                var xmlPathsFromOneFolder = GetAllXmlPaths(folderPath);
                xmlPathsFromAllFolders.AddRange(xmlPathsFromOneFolder);
            }

            foreach (var xmlPath in xmlPathsFromAllFolders)
            {

                metadataItemList.Add(this._documentService.DeserializeMetadataItem(_documentService.SerializeTestable, (string)xmlPath));
            }

            this.MetadataItems = metadataItemList.Cast<MetadataItem>().ToList();
            return this.MetadataItems;
        }

        private static IEnumerable<string> GetAllFolderPaths(string targetPath)
        {
            return Directory.GetDirectories(targetPath);
        }

        private static ArrayList GetAllXmlPaths(string folderPath)
        {
            var xmlPaths = new ArrayList();
            foreach (var file in Directory.EnumerateFiles(folderPath, "*.xml"))
            {
                xmlPaths.Add(file);
            }

            return xmlPaths;
        }
        #endregion
    }
}
