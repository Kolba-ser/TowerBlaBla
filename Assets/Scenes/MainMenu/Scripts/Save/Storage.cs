using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace MainMenu.Save
{
    //Можно сохранять данные при переходе не сцену
    public sealed class Storage
    {

        private const string DATA_PATH = @"C:\Users\Пользователь\TowerBlaBla\Assets\SavedData\Settings.xml";

        private XmlSerializer _xmlSerializer;

        public Storage()
        {
            _xmlSerializer = new XmlSerializer(typeof(SettingsData));
        }

        public void SaveData(object data)
        {
            if (IsExists(DATA_PATH))
            {
                Truncate(data);
                return;
            }

            using (FileStream fileStream = new FileStream(DATA_PATH, FileMode.Open))
            {
                Serialize(fileStream, data);
            }

        }
        public bool GetData(ref SettingsData data)
        {
            if (IsExists(DATA_PATH) && IsEmpty(DATA_PATH) == false)
            {

                using (FileStream fileStream = new FileStream(DATA_PATH, FileMode.Open))
                {
                    data = (SettingsData)_xmlSerializer.Deserialize(fileStream);
                    return true;
                }
            }

            data = new SettingsData();
            return false;

        }

        private void Serialize(FileStream fileStream, object data)
        {
            if (data == null)
            {
                data = new SettingsData();
            }

            _xmlSerializer.Serialize(fileStream, data);
        }

        private bool IsEmpty(string path)
        {
            int lenght = File.ReadAllLines(path).Length;
            if (lenght == 0)
            {
                return true;
            }
            return false;
        }
        private bool IsExists(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }

            File.Create(path);
            return false;
        }

        private async void Truncate(object data)
        {
            await Task.Run(() =>
            { 
                using (FileStream fileStream = new FileStream(DATA_PATH, FileMode.Truncate))
                {
                    Serialize(fileStream, data);
                }
            });
        }
    }

}