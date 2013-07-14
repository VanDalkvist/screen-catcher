using ScreenCatcher.Core;
using ScreenCatcher.Model;

namespace ScreenCatcher.Logic
{
    public class DefaultEditorProvider : IEditorProvider
    {
        public IEditor Create(CatcherSettings settings)
        {
            if (settings.CurrentProgramm == Programm.Paint)
                return new PaintEditor();

            return null;
        }
    }
}