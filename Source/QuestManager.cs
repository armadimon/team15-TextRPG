using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source.State;

namespace _15TextRPG.Source
{
    public class QuestManager
    {
        private static QuestManager instance;
        public static QuestManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new QuestManager();
                return instance;
            }
        }

        public List<Quest> Quests { get; set; }


        public QuestManager()
        {
            Quests = new List<Quest>();
        }
        public void AddQuest(Quest quest)
        {
            Quests.Add(quest);
        }

        public void UpdateQuest(string questId, int objectIndex)
        {
            Quest quest = Quests.FirstOrDefault(q => q.Name == questId);

            Console.WriteLine($"{quest}");
            Console.ReadLine();
            if (quest == null)
                return;
            quest.Object[objectIndex].UpdateProgress();
            quest.CheckComplete();
            Console.WriteLine($"{quest.Status}");

            Console.ReadLine();
            if (quest.Status == QuestStatus.Completed)
            {
                Console.WriteLine($"퀘스트 '{quest.Name}' 완료!");
                Console.ReadLine();
            }
        }
        // 퀘스트 수락
        public void AcceptQuest(Quest quest)
        {
            Console.WriteLine($"퀘스트 '{quest.Name}' 수락했습니다!");
            quest.Start();
        }

        // 퀘스트 거절
        //public void RejectQuest(Quest quest)
        //{
        //}

        public void ShowQuest(Quest quest)
        {
            Console.WriteLine($"퀘스트 제목: {quest.Name}");
            Console.WriteLine($"연관 챕터: {quest.ChapterName}");
            Console.WriteLine($"퀘스트 설명: {quest.Description}");
            Console.WriteLine($"보상: {1}");
        }
    }
}
