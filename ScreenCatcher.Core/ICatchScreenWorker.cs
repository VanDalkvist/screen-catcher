using ScreenCatcher.Model;

namespace ScreenCatcher.Core
{
    public interface ICatchScreenWorker
    {
        void Catch(CatcherSettings settings, out string fileName);
    }
}