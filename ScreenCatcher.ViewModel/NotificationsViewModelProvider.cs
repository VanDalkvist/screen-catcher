using ScreenCatcher.Logic;

namespace ScreenCatcher.ViewModel
{
    public class NotificationsViewModelProvider : IViewModelProvider
    {
        private readonly string _fileName;

        public NotificationsViewModelProvider(string fileName)
        {
            _fileName = fileName;
        }

        public ViewModelBase Create()
        {
            return new NotificationsViewModel(new EditorProvider(), new DefaultCatalogViewerProvider(), _fileName);
        }
    }
}