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
    }

    public class Quest
    {
        public string Name { get; }
        public string Description { get; }
        public ChapterID ChapterName { get; set; }
        public List<IQuestObject> Object { get; } = new List<IQuestObject>();
        public QuestStatus Status { get; private set; }

        public Quest(string name, ChapterID chapterName, string description)
        {
            Name = name;
            ChapterName = chapterName;
            Description = description;
            Status = QuestStatus.NotStarted;
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
    }
}
