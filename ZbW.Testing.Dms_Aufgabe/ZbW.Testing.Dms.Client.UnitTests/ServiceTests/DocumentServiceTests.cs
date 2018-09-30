using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.UnitTests.ServiceTests
{
    [TestFixture]
    internal class DocumentServiceTests
    {
        [Test]
        public void MetadataItem_Serialize_Success()
        {
            // arrange
            var documentService = new DocumentService();
            var metadataItemStub = A.Fake<MetadataItem>();
            var xmlTestMock = A.Fake<Serialize>();

            // act
            var result = documentService.SerializeMetadataItem(xmlTestMock, metadataItemStub);

            // assert
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void MetaDataItem_Deserialize_Success()
        {
            // arrange
            var documentService = new DocumentService();
            const string path = "path";
            var xmlTestableMock = A.Fake<Serialize>();

            // act
            var result = documentService.DeserializeMetadataItem(xmlTestableMock, path);

            // assert
            Assert.That(result, Is.Not.Null);
        }
        //[Test]
        //public void File_OpenFile_Success()
        //{
        //    // arrange
        //    var documentService = new DocumentService();
        //    var metaDataStub = A.Fake<MetadataItem>();
        //    var fileTestableMock = A.Fake<Serialize>();


        //    // act
        //    documentService.OpenFile(fileTestableMock, metaDataStub);

        //    // assert
        //    A.CallTo(() => documentService.OpenFile(fileTestableMock, metaDataStub)).MustHaveHappenedOnceExactly();
        //}
        [Test]
        public void File_GenerateContentFileName_RightName()
        {
            // arrange
            var documentId = new Guid("6efa152a-b189-44b2-ac0f-c40fb413d3c2");
            const string extension = ".pdf";
            var documentService = new DocumentService();

            // act
            var result = documentService.GetContentFileName(documentId, extension);

            // assert
            Assert.That(result, Is.EqualTo(documentId + "_Content" + extension));
        }
        [Test]
        public void File_GenerateMetaDataFileName_CorrectName()
        {
            // arrange
            var documentId = new Guid("cea4fadb-685d-40d5-93db-6cabd64cd3d0");
            var documentService = new DocumentService();

            // act
            var result = documentService.GetMetadataFileName(documentId);

            // assert
            Assert.That(result, Is.EqualTo(documentId + "_Metadata.xml"));
        }
    }
}
