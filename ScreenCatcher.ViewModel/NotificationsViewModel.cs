using System;
using System.Windows.Input;

using ScreenCatcher.Common;
using ScreenCatcher.Core;
using ScreenCatcher.Logic;

namespace ScreenCatcher.ViewModel
{
    public class NotificationsViewModel : ViewModelBase
    {
        protected readonly IEditorProvider _editorProvider;
        protected readonly ICatalogViewerProvider _catalogViewerProvider;

        private readonly string _fileName;

        public NotificationsViewModel(IEditorProvider editorProvider, ICatalogViewerProvider catalogViewerProvider, string fileName)
        {
            _editorProvider = editorProvider;
            _catalogViewerProvider = catalogViewerProvider ?? new DefaultCatalogViewerProvider();
            _fileName = fileName;
        }

        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get { return _editCommand ?? (_editCommand = new RelayCommand(Edit)); }
        }

        private void Edit(object arg)
        {
            var fileName = arg as string;
            if (string.IsNullOrEmpty(fileName))
                return;

            var settings = SettingsProvider.GetCatcherSettings();
            var editor = _editorProvider.Create(settings);
            editor.Edit(fileName);
        }

        private ICommand _openDirectoryCommand;
        public ICommand OpenDirectoryCommand
        {
            get { return _openDirectoryCommand ?? (_openDirectoryCommand = new RelayCommand(OpenDirectory)); }
        }

        private void OpenDirectory(object obj)
        {
            var path = obj as string;
            if (string.IsNullOrEmpty(path))
                return;

            var settings = SettingsProvider.GetCatcherSettings();
            var catalogViewer = _catalogViewerProvider.Create(settings);
            catalogViewer.View(path);
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public string FullFileName
        {
            get { return Environment.CurrentDirectory + Constants.Delimiter + _fileName; }
        }
    }
}