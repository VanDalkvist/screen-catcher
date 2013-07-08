using System.Windows;
using System.Windows.Forms;
using SystemModifierKeys = System.Windows.Input.ModifierKeys;

using ScreenCatcher.Core;
using ModifierKeys = ScreenCatcher.Model.ModifierKeys;

namespace ScreenCatcher.Logic
{
    public class ScreenCatcher
    {
        private readonly HotKeyCatcher _hotKeyCatcher;
        private readonly HotKeyStorage _hotKeyStorage;

        private readonly IEditorProvider _editorProvider;

        public ScreenCatcher(Window window)
        {
            _editorProvider = new EditorProvider();

            if (_hotKeyCatcher == null)
            {
                _hotKeyCatcher = new HotKeyCatcher(window);
                _hotKeyStorage = new HotKeyStorage();

                Subscribe();
            }
            else
                Unload();

            Register();
        }

        private void Subscribe()
        {
            _hotKeyCatcher.Captured += OnCaptured;
        }

        private void OnCaptured(object sender, CatcherEventHandlerArgs args)
        {
            Catch(args.Key);
        }

        private void Register()
        {
            var settings = SettingsProvider.GetCatcherSettings();
            if (settings.ScreenCatch.Key != Keys.None)
            {
                var key = RegisterKey(settings.ScreenCatch.Key, settings.ScreenCatch.ModifierKey);
                _hotKeyStorage.Add(key, new CatchScreenWorker());
            }

            if (settings.ScreenCatchCurrentWindow.Key != Keys.None)
            {
                var key = RegisterKey(settings.ScreenCatchCurrentWindow.Key, settings.ScreenCatchCurrentWindow.ModifierKey);
                _hotKeyStorage.Add(key, new CatchCurrentWindowWorker());
            }

            if (settings.ScreenCatchWithConfirmation.Key != Keys.None)
            {
                var key = RegisterKey(settings.ScreenCatchWithConfirmation.Key, settings.ScreenCatchWithConfirmation.ModifierKey);
                _hotKeyStorage.Add(key, new CatchScreenWithConfirmationWorker());
            }
        }

        private short RegisterKey(Keys key, ModifierKeys modifierKey)
        {
            return _hotKeyCatcher.RegisterGlobalHotkey(key, (SystemModifierKeys)modifierKey);
        }

        private void Catch(short key)
        {
            var settings = SettingsProvider.GetCatcherSettings();

            string fileName;
            _hotKeyStorage[key].Catch(settings, out fileName);
            if (settings.RunAs)
            {
                var editor = _editorProvider.Create(settings);
                editor.Edit(fileName);
            }

            if (settings.UseNotification)
                Notify(fileName);
        }

        private void Notify(string fileName)
        {
            OnNotifying(fileName);
        }

        public void Unload()
        {
            _hotKeyCatcher.UnregisterHotkeys(_hotKeyStorage);
            _hotKeyStorage.Clear();
        }

        public event NotifyEventHandler Notifying;

        private void OnNotifying(NotifyEventHandlerArgs args)
        {
            var handler = Notifying;
            if (handler != null)
                handler(this, args);
        }

        private void OnNotifying(string fileName)
        {
            OnNotifying(new NotifyEventHandlerArgs(fileName));
        }
    }
}