using ScreenCatcher.Configuration;
using ScreenCatcher.ViewModel;

namespace ScreenCatcher.View
{
    public partial class Notification
    {
        public Notification()
            : this(UnitySingleton<ScreenCatcherViewModel>.Instance)
        {

        }

        public Notification(ViewModelBase viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}