using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

using ScreenCatcher.Common;

namespace ScreenCatcher.Model
{
    public class Serializer : SingletonInstance<Serializer>
    {
        public void Serialize(object data, string fileName = null)
        {
            var storeName = string.IsNullOrEmpty(fileName) ? Constants.SettingsFileName : fileName;

            using (var writer = new StreamWriter(storeName))
            {
                var serializer = new XmlSerializer(typeof(ScreenSettings));
                serializer.Serialize(writer, data);
            }
        }

        public object Deserialize(string fileName)
        {
            var serializer = new XmlSerializer(typeof(ScreenSettings));

            if (!File.Exists(fileName))
                return null;

            object deserialized;

            using (var reader = new StreamReader(fileName))
            {
                try
                {
                    deserialized = serializer.Deserialize(reader);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error during configuration loading. Default settings will be applied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            return deserialized;
        }
    }
}