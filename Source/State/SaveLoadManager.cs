using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _15TextRPG.Source.State
{
    internal class SaveLoadManager()
    {
        List<ISaveable> observers = new();

        public void AddSaveableEntity<T>(T target, string fileName)
        {
            SaveableEntity<T> observer = new(target, fileName);
            observers.Add(observer);
        }

        public override string ToString()
        {
            string result = "DATA: ";
            foreach (ISaveable t in observers)
            {
                result += t.ToString();
            }
            return result;
        }

        public void DoSave()
        {
            foreach (ISaveable t in observers) 
            { 
                t.Save();
            }
        }

        public void DoSave(string fileName)
        {
            foreach (ISaveable t in observers)
            {
                if(t.FileName == fileName)
                    t.Save();
            }
        }

        public void DoLoad()
        {
            foreach (ISaveable t in observers)
            {
                t.Load();
            }
        }

        public void DoLoad(string fileName)
        {
            foreach (ISaveable t in observers)
            {
                if (t.FileName == fileName)
                    t.Load();
            }
        }
    }

    interface ISaveable
    {
        string FileName { get; }
        bool Save();
        bool Load();
        string ToString();
    }

    class SaveableEntity<T> : ISaveable
    {
        public T Target { get; private set; }
        public string FileName { get; set; } = "EMPTY";

        public SaveableEntity(T target, string fileName)
        {
            Target = target;
            FileName = fileName;
        }

        public bool Save()
        {
            string fileName = FileName + ".json";
            string jsonString = JsonSerializer.Serialize(Target);
            File.WriteAllText(fileName, jsonString);
            return true;
        }

        public bool Load()
        {
            string fileName = FileName + ".json";
            if(!File.Exists(fileName))
            {
                Console.WriteLine($"{fileName}을 불러오는데 실패했습니다.");
            }
            else
            {
                string jsonString = File.ReadAllText(fileName);
                T? loadedT = JsonSerializer.Deserialize<T>(jsonString);
                Console.WriteLine($"{fileName} 불러오기 성공!");
                Target = loadedT;
            }
            return true;
        }

        public override string ToString() => JsonSerializer.Serialize(Target);
    }
}
