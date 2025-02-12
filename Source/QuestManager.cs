using System;
using System.Collections;
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
            if (quest == null)
                return;
            quest.Object[objectIndex].UpdateProgress();
            quest.CheckComplete();

            Console.ReadLine();
            if (quest.Status == QuestStatus.Completed)
            {
                Console.WriteLine($"퀘스트 '{quest.Name}' 완료!");
                Console.ReadLine();
            }
        }
        public void CheckQuest()
        {
            
            foreach (Quest quest in Quests)
            {
                quest.CheckComplete();
                  }
                

        }

        public void AcceptQuest(Quest quest)
        {
            Console.WriteLine($"퀘스트 '{quest.Name}' 수락했습니다!");
            quest.Start();
        }


        public void ShowQuest(Quest quest)
        {
            Console.WriteLine($"퀘스트 제목: {quest.Name}");
            Console.WriteLine($"연관 챕터: {quest.ChapterName}");
            Console.WriteLine($"퀘스트 설명: {quest.Description}");
            if (quest.Object.Count > 0)
            {
                Console.WriteLine("퀘스트 목표:");
                foreach (var obj in quest.Object)
                {
                    obj.ShowProgress();
                }
            }
        }
    }
}
