using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    public enum QuestStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    public interface IQuestObject
    {
        bool IsCompleted { get; }
        void UpdateProgress();
        void ShowProgress();
    }

    public class Quest
    {
        public string Name { get; }
        public string Description { get; }
        public ChapterID ChapterName { get; set; }
        public List<IQuestObject> Object { get; } = new List<IQuestObject>();
        public QuestStatus Status { get; private set; }
        public Reward Reward { get; set; }

        public Quest(string name, ChapterID chapterName, string description, int rewardExp, int rewardGold, List<IItem>? items = null)
        {

            Name = name;
            ChapterName = chapterName;
            Description = description;
            Status = QuestStatus.NotStarted;
            if (items == null)
                items = new List<IItem>();
            Reward = new Reward(items, rewardExp, rewardGold);
        }

        public void Start()
        {
            if (Status == QuestStatus.NotStarted)
                Status = QuestStatus.InProgress;
        }

        public void CheckComplete()
        {
            if (Status == QuestStatus.InProgress && Object.All(o => o.IsCompleted))
                Status = QuestStatus.Completed;
        }

        public void ShowRewards()
        {
            Console.WriteLine($"[보상]\n");
            Console.WriteLine($"Exp: {Reward.rewardExp}");
            Console.WriteLine($"Gold: {Reward.rewardGold}");
            foreach( var item in Reward.Items)
            {
                Console.WriteLine($"아이템 : [{item.Name}] : {item.Desc}");
            }
            Console.WriteLine();
        }
    }
}
