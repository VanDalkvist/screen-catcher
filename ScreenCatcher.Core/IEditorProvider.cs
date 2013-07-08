using ScreenCatcher.Model;

namespace ScreenCatcher.Core
{
    public interface IEditorProvider
    {
        IEditor Create(CatcherSettings settings);
    }
}