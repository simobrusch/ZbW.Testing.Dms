using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Services;
using ZbW.Testing.Dms.Client.ViewModels;

namespace ZbW.Testing.Dms.Client.IntegrationsTests
{
    [TestFixture]
    internal class DocumentServiceTests
    {
        private const string Benutzer = "simo";
        private const string Testpfad = "TestPath";
        [Test]
        public void Document_Search_Success()
        {
            // arrange
            var documentServiceModel = new DocumentDetailViewModel(Benutzer, null);

            // act
            var result = documentServiceModel.CmdDurchsuchen.CanExecute();

            // assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Document_DocumentSelected_ReturnTrue()
        {
            // arrange
            var documentServiceModel = new DocumentDetailViewModel(Benutzer, null)
            {
                FilePath = Testpfad
            };

            // act
            var result = documentServiceModel.DocumentIsSelected();

            // assert
            Assert.That(result, Is.True);

        }

        [Test]
        public void Document_HasAllRequiredFields_ReturnTrue()
        {
            // arrange
            var documentServiceModel = new DocumentDetailViewModel(Benutzer, null)
            {
                Bezeichnung = Testpfad,
                ValutaDatum = DateTime.Now,
                SelectedTypItem = "verträge"
            };

            // act
            var result = documentServiceModel.RequiredFields();

            // assert
            Assert.That(result, Is.True);

        }

        [Test]
        public void Document_CreateMetadataItem_Success()
        {
            //arrange
            var documentServiceModel = new DocumentDetailViewModel(Benutzer, null)
            {
                Bezeichnung = "Test",
                ValutaDatum = DateTime.Now,
                SelectedTypItem = "verträge",
                StichWoerter = "verträge",
                IsRemoveFileEnabled = false,
                FilePath = Testpfad
            };
            var xmlTestMock = A.Fake<MetadataItem>();

            //act
            var result = documentServiceModel.CreateMetadataItem(xmlTestMock);

            //assert
            Assert.That(result, Is.Not.Null);

        }

    }
}
