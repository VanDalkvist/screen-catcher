using ScreenCatcher.Logic;

namespace ScreenCatcher.ViewModel
{
    public class SettingsViewModelProvider : IViewModelProvider
    {
        private readonly ViewModelBase _mainViewModel;

        public SettingsViewModelProvider(ViewModelBase mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public ViewModelBase Create()
        {
            var screenCatcherViewModel = _mainViewModel as ScreenCatcherViewModel;
            if (screenCatcherViewModel == null)
                return null;

            var settings = SettingsProvider.GetCatcherSettings();
            return new SettingsViewModel(settings);
        }
    }
}