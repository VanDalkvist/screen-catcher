using ScreenCatcher.Configuration;
using ScreenCatcher.ViewModel;

namespace ScreenCatcher.View
{
    public partial class Settings
    {
        public Settings()
            : this(UnitySingleton<SettingsViewModel>.Instance)
        {
            InitializeComponent();
        }

        public Settings(SettingsViewModel viewModel)
        {
            DataContext = viewModel;
        }
    }
}