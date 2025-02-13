using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    internal class SaveLoadManager()
    {
        List<ISaveable> saveableEntitys = [];
        public void AddSaveableEntity<T>(T target, string fileName) where T : class
        {
            SaveableEntity<T> saveableEntity = new(target, fileName);
            saveableEntitys.Add(saveableEntity);
        }

        public void DoSave() => saveableEntitys.ForEach(t => t.Save());

        public void DoSave(string fileName) => saveableEntitys.ForEach(t => { if (t.FileName == fileName) t.Save(); });

        public void DoLoad() => saveableEntitys.ForEach(t => t.Load());

        public void DoLoad(string fileName) => saveableEntitys.ForEach(t => { if (t.FileName == fileName) t.Load(); });

        public string ToString(JsonSerializerOptions option)
        {
            string result = "";
            foreach (ISaveable t in saveableEntitys)
            {
                result += t.FileName + ": " + t.ToString();
            }
            return result;
        }

        private interface ISaveable
        {
            string FileName { get; }
            bool Save();
            bool Load();
            string ToString(JsonSerializerOptions option);
        }

        private class SaveableEntity<T>(T target, string fileName) : ISaveable where T : class
        {
            public string FileName { get; set; } = fileName;

            public bool Save()
            {
                string fileName = FileName + ".json";
                string jsonString = JsonSerializer.Serialize(target);
                File.WriteAllText(fileName, jsonString);
                return true;
            }

            public bool Load()
            {
                string fileName = FileName + ".json";
                if (!File.Exists(fileName))
                {
                    Console.WriteLine($"{fileName}을 불러오는데 실패했습니다.");
                }
                else
                {
                    string jsonString = File.ReadAllText(fileName);
                    T? loadedT = JsonSerializer.Deserialize<T>(jsonString);
                    Console.WriteLine($"{fileName} 불러오기 성공!");
                    if (loadedT != null)
                    {
                        foreach (var prop in typeof(T).GetProperties())
                        {
                            if (prop.CanWrite)
                            {
                                prop.SetValue(target, prop.GetValue(loadedT));
                            }
                        }
                    }
                }
                return true;
            }

            public string ToString(JsonSerializerOptions? option) => JsonSerializer.Serialize(target, option);
        }
    }
}
