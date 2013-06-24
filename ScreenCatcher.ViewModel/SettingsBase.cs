using System;

namespace ScreenCatcher.ViewModel
{
    public abstract class SettingsBase : ICloneable
    {
        private const string StoreFileName = "settings.xml";

        public void Save()
        {
            Serializer.Instance.Serialize(this, StoreFileName);
        }

        public static TSettings Load<TSettings>()
        {
            return (TSettings)Serializer.Instance.Deserialize(StoreFileName);
        }

        public abstract object Clone();
    }
}