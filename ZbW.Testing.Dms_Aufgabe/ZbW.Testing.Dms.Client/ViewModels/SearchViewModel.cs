using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Prism.Commands;
    using Prism.Mvvm;

    using ZbW.Testing.Dms.Client.Model;
    using ZbW.Testing.Dms.Client.Repositories;

    internal class SearchViewModel : BindableBase
    {
        private List<MetadataItem> _filteredMetadataItems;
        private MetadataItem _selectedMetadataItem;
        private string _selectedTypItem;
        private string _suchbegriff;
        private List<string> _typItems;
        private readonly DocumentService _documentService;
        private readonly SearchDocument _searchDocument;

        public SearchViewModel()
        {
            TypItems = ComboBoxItems.Typ;
            CmdSuchen = new DelegateCommand(OnCmdSuchen);
            CmdReset = new DelegateCommand(OnCmdReset);
            CmdOeffnen = new DelegateCommand(OnCmdOeffnen, OnCanCmdOeffnen);
            _searchDocument = new SearchDocument();
            FilteredMetadataItems = _searchDocument.GetAllMetadataItems();
            _documentService = new DocumentService();
        }
        #region Properties
        public DelegateCommand CmdOeffnen { get; }
        public DelegateCommand CmdSuchen { get; }
        public DelegateCommand CmdReset { get; }

        public string Suchbegriff
        {
            get => _suchbegriff;
            set => SetProperty(ref _suchbegriff, value);
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

        public List<MetadataItem> FilteredMetadataItems
        {
            get => _filteredMetadataItems;
            set => SetProperty(ref _filteredMetadataItems, value);
        }

        public MetadataItem SelectedMetadataItem
        {
            get => _selectedMetadataItem;
            set
            {
                if (SetProperty(ref _selectedMetadataItem, value))
                {
                    CmdOeffnen.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Methods
        private bool OnCanCmdOeffnen()
        {
            return SelectedMetadataItem != null;
        }

        private void OnCmdOeffnen()
        {
            _documentService.OpenFile(_documentService.SerializeTestable, SelectedMetadataItem);
        }

        private void OnCmdSuchen()
        {
            FilteredMetadataItems = _searchDocument.FilterMetadataItems(_selectedTypItem, _suchbegriff);
        }

        private void OnCmdReset()
        {
            FilteredMetadataItems = _searchDocument.MetadataItems;
            Suchbegriff = string.Empty;
            SelectedTypItem = null;
        }
        #endregion
    }
}