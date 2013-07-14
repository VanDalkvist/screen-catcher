using System.Windows;

using ScreenCatcher.Configuration;
using ScreenCatcher.Core;
using ScreenCatcher.ViewModel;

namespace ScreenCatcher.View.Controls
{
    public partial class Catcher
    {
        public Catcher()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HadCaughtProperty =
            DependencyProperty.Register("HadCaught", typeof(string), typeof(Catcher), new PropertyMetadata(HadCaughtChanged));

        public string HadCaught
        {
            get { return (string)GetValue(HadCaughtProperty); }
            set { SetValue(HadCaughtProperty, value); }
        }

        private static void HadCaughtChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fileName = (string)e.NewValue;
            if (!string.IsNullOrEmpty(fileName))
                Notify(fileName);
        }

        private static void Notify(string fileName)
        {
            var editorProvider = UnitySingleton<IEditorProvider>.Instance;
            var catalogViewerProvider = UnitySingleton<ICatalogViewerProvider>.Instance;
            var viewModel = new NotificationsViewModel(editorProvider, catalogViewerProvider, fileName);
            var notification = new Notification(viewModel);
            notification.Show();
        }
    }
}