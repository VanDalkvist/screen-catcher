using System.Windows;

using ScreenCatcher.Configuration;
using ScreenCatcher.ViewModel;

namespace ScreenCatcher.View
{
    public partial class Notification
    {
        public Notification() : this(UnitySingleton<ScreenCatcherViewModel>.Instance) { }

        public Notification(ViewModelBase viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Top = SystemParameters.FullPrimaryScreenHeight - Height - 40;
        }
    }
}