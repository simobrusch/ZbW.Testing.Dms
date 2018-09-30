using System.Windows;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Win32;

    using Prism.Commands;
    using Prism.Mvvm;

    using ZbW.Testing.Dms.Client.Repositories;

    public class DocumentDetailViewModel : BindableBase
    {
        #region variables

        private readonly Action _navigateBack;
        private string _benutzer;
        private string _bezeichnung;
        private DateTime _erfassungsdatum;

        public string FilePath { get; set; }
        private bool _isRemoveFileEnabled;
        private string _selectedTypItem;
        private string _stichWoerter;
        private List<string> _typItems;
        private DateTime? _valutaDatum;
        private readonly MetadataItem _metadataitems;

        #endregion

        public DocumentDetailViewModel(string benutzer, Action navigateBack)
        {
            _navigateBack = navigateBack;
            Benutzer = benutzer;
            Erfassungsdatum = DateTime.Now;
            ValutaDatum = DateTime.Today;
            TypItems = ComboBoxItems.Typ;
            CmdDurchsuchen = new DelegateCommand(OnCmdDurchsuchen);
            CmdSpeichern = new DelegateCommand(OnCmdSpeichern);
            _metadataitems = new MetadataItem();
        }

        #region Properties
        public string StichWoerter
        {
            get => _stichWoerter;
            set => SetProperty(ref _stichWoerter, value);
        }
        public string Bezeichnung
        {
            get => _bezeichnung;
            set => SetProperty(ref _bezeichnung, value);
        }
        public List<string> TypItems
        {
            get => _typItems;
            set => SetProperty(ref _typItems, value);
        }
        public string SelectedTypItem
        {
            get => _selectedTypItem;
            set => SetProperty(ref _selectedTypItem, value);
        }
        public DateTime Erfassungsdatum
        {
            get => _erfassungsdatum;
            set => SetProperty(ref _erfassungsdatum, value);
        }
        public string Benutzer
        {
            get => _benutzer;
            set => SetProperty(ref _benutzer, value);
        }
        public DelegateCommand CmdDurchsuchen { get; }
        public DelegateCommand CmdSpeichern { get; }

        public DateTime? ValutaDatum
        {
            get => _valutaDatum;
            set => SetProperty(ref _valutaDatum, value);
        }

        public bool IsRemoveFileEnabled
        {
            get => _isRemoveFileEnabled;
            set => SetProperty(ref _isRemoveFileEnabled, value);
        }

        #endregion

        #region Methods
        private void OnCmdDurchsuchen()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();

            if (result.GetValueOrDefault())
            {
                FilePath = openFileDialog.FileName;
            }
        }
        private void OnCmdSpeichern()
        {
            var metadataItem = new MetadataItem();

            // überprüfen ob die Pflichtfelder ausgefüllt worden sind:
            if (!this.RequiredFields())
            {
                MessageBox.Show("Bitte füllen Sie alle Pflichtfelder aus bevor sie ein Dokument speichern.", "Warnung!");
                return;
            }
            _metadataitems.AddFile(CreateMetadataItem(metadataItem), _isRemoveFileEnabled);
            _navigateBack();

        }

        public MetadataItem CreateMetadataItem(MetadataItem serializeTestableMock)
        {
            var metadataItem = new MetadataItem
            {
                Benutzer = Benutzer,
                Bezeichnung = Bezeichnung,
                OriginalPath = this.FilePath,
                IsRemoveFileEnabled = this.IsRemoveFileEnabled,
                Tag = this.StichWoerter,
                Typ = this.SelectedTypItem,
                ValutaDatum = (DateTime)ValutaDatum,
                ErstellungsDatum = DateTime.Now
            };

            return metadataItem;
        }

        public bool RequiredFields()
        {
            return !string.IsNullOrEmpty(this.Bezeichnung) && this.ValutaDatum.HasValue &&
                   !string.IsNullOrEmpty(this.SelectedTypItem);
        }

        public bool DocumentIsSelected()
        {
            return !string.IsNullOrEmpty(this.FilePath);
        }

        #endregion
    }
}