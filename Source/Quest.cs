using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    public class Quest
    {
        int ID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int Exp { get; set; }
        int Gold { get; set; }
        bool IsCompleted { get; set; }
        bool IsRewarded { get; set; }

        public Quest(int id, string name, string description, int exp, int gold)
        {
            ID = id;
            Name = name;
            Description = description;
            Exp = exp;
            Gold = gold;
            IsCompleted = false;
            IsRewarded = false;
        }

        public void ShowQuest()
        {
            Console.WriteLine($"[{Name}]");
            Console.WriteLine($"의뢰 내용 : {Description}");
            Console.WriteLine($"퀘스트 보상: 경험치 {Exp}, 크레딧 {Gold}");
        }

        public void Complete()
        {
            IsCompleted = true;
        }

        public void Reward()
        {
            IsRewarded = true;
        }
    }
}
