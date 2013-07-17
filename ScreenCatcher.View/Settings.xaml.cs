using ScreenCatcher.Configuration;
using ScreenCatcher.ViewModel;

namespace ScreenCatcher.View
{
    public partial class Settings
    {
        public Settings() : this(Configurator.GetInstance<SettingsViewModel>()) { }

        public Settings(SettingsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}