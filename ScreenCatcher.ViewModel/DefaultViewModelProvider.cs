namespace ScreenCatcher.ViewModel
{
    public class DefaultViewModelProvider : IViewModelProvider
    {
        public ViewModelBase Create()
        {
            return new ViewModelBase();
        }
    }
}