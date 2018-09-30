using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NUnit.Framework;
using FakeItEasy;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Services;
using ZbW.Testing.Dms.Client.ViewModels;

namespace ZbW.Testing.Dms.Client.UnitTests.ModelTests
{
    [TestFixture]
    internal class MetadataItemTests
    {
        [Test]
        public void DetailsOfDocument_AddingFile_ReturnSuccess()
        {
            // arrange
            var serializeTestableMock = A.Fake<MetadataItem>();
            var doc = new DocumentDetailViewModel("simo", null);
            var metadataItem = doc.CreateMetadataItem(serializeTestableMock);

            // act
            doc.Bezeichnung = "TestFile";
            doc.SelectedTypItem = "Verträge";
            doc.ValutaDatum = new DateTime(2018, 1, 1, 00, 0, 0);
            doc.StichWoerter = "vertraege";
            doc.IsRemoveFileEnabled = false;

            // assert
            Assert.That(metadataItem, Is.Not.Null);
        }
    }
}
