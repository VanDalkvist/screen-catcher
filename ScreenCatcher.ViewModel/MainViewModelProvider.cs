namespace ScreenCatcher.ViewModel
{
    public class MainViewModelProvider : IViewModelProvider
    {
        public ViewModelBase Create()
        {
            return new ScreenCatcherViewModel();
        }
    }
}