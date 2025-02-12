using System;

namespace _15TextRPG.Source.State
{
    public class CheckContractState : IGameState
    {
        ChapterID ChapterName { get; set; }
        private List<Quest> questLists;
        public CheckContractState(ChapterID chapterName)
        {
            ChapterName = chapterName;
            questLists = QuestManager.Instance.Quests.Where(q => q.ChapterName == chapterName).ToList();
        }

        public void DisplayMenu()
        {
            Console.Clear();
            QuestManager.Instance.CheckQuest();
            questLists = QuestManager.Instance.Quests.Where(q => q.ChapterName == ChapterName).ToList();
            CheckContract(ChapterName);
        }

        public void HandleInput()
        {
            Console.Write("\n원하는 외뢰를 골라주세요. >> ");
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int questIndex) && questIndex >= 0 && questIndex <= questLists.Count)
            {
                switch (input)
                {
                    case "0":
                        GameManager.Instance.ChangeState(new ContractState());
                        break;
                    default:
                        if (questLists[questIndex - 1].Status == QuestStatus.NotStarted)
                            DisplayAcceptMenu(questIndex);
                        else if (questLists[questIndex - 1].Status == QuestStatus.InProgress)
                            DisplayProgressMenu(questIndex);
                        else if (questLists[questIndex - 1].Status == QuestStatus.Completed)
                            DisplayRewardMenu(questIndex);
                        break;
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 선택하세요.");
                Console.ReadLine();
            }
        }

        public void CheckContract(ChapterID chapterName)
        {
            Console.Clear();

            Console.WriteLine($"=== {chapterName} 의뢰 목록 ===");
            for (int i = 0; i < questLists.Count; i++)
            {
                if (questLists[i].Status == QuestStatus.NotStarted)
                    Console.Write("[미수주] ");
                else if (questLists[i].Status == QuestStatus.InProgress)
                    Console.Write("[진행중] ");
                else if (questLists[i].Status == QuestStatus.Completed)
                    Console.Write("[완료]  ");
                Console.WriteLine($"{i + 1}. {questLists[i].Name}");
            }
            if (questLists.Count == 0)
            {
                Console.WriteLine($"현재 {chapterName}에서 수락 가능한 퀘스트가 없습니다.");
            }
            Console.WriteLine("0. 돌아가기");
        }

        public void DisplayAcceptMenu(int questIndex)
        {
                Console.Clear();
                Quest selectedQuest = questLists[questIndex - 1];
                QuestManager.Instance.ShowQuest(selectedQuest);

                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");
                string input = Console.ReadLine() ?? "";

                if (input == "1")
                {
                    QuestManager.Instance.AcceptQuest(selectedQuest);
                    Console.WriteLine("퀘스트를 수락했습니다.");
                    Console.ReadLine();
                }
                else if (input == "2")
                {
                    Console.WriteLine("퀘스트를 거절했습니다.");
                    Console.ReadLine();
                }
        }
        public void DisplayProgressMenu(int questIndex)
        {
            Console.Clear();
            Quest selectedQuest = questLists[questIndex - 1];
            QuestManager.Instance.ShowQuest(selectedQuest);
            foreach (var obj in selectedQuest.Object)
            {
                obj.ShowProgress();
            }
            Console.WriteLine("엔터를 눌러 계속하세요");
            Console.ReadLine();
        }

        public void DisplayRewardMenu(int questIndex)
        {
            Console.Clear();
            Quest selectedQuest = questLists[questIndex - 1];
            QuestManager.Instance.ShowQuest(selectedQuest);

            Console.WriteLine("1. 보상 받기");
            Console.WriteLine("2. 돌아오기");
            string input = Console.ReadLine() ?? "";

            if (input == "1")
            {
                Console.WriteLine("보상을 획득했습니다.");
                QuestManager.Instance.Quests.Remove(selectedQuest); ;
                Console.ReadLine();
            }
            else if (input == "2")
            {
            }
        }
    }
}