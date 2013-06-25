using System;
using ScreenCatcher.Common;

namespace ScreenCatcher.Model
{
    public abstract class SettingsBase : ICloneable
    {
        public void Save()
        {
            Serializer.Instance.Serialize(this, Constants.SettingsFileName);
        }

        public static TSettings Load<TSettings>()
        {
            return (TSettings)Serializer.Instance.Deserialize(Constants.SettingsFileName);
        }

        public abstract object Clone();
    }
}