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
            Console.Write("\n���ϴ� �ܷڸ� ����ּ���. >> ");
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
                Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �����ϼ���.");
                Console.ReadLine();
            }
        }

        public void CheckContract(ChapterID chapterName)
        {
            Console.Clear();

            Console.WriteLine($"=== {chapterName} �Ƿ� ��� ===");
            for (int i = 0; i < questLists.Count; i++)
            {
                if (questLists[i].Status == QuestStatus.NotStarted)
                    Console.Write("[�̼���] ");
                else if (questLists[i].Status == QuestStatus.InProgress)
                    Console.Write("[������] ");
                else if (questLists[i].Status == QuestStatus.Completed)
                    Console.Write("[�Ϸ�]  ");
                Console.WriteLine($"{i + 1}. {questLists[i].Name}");
            }
            if (questLists.Count == 0)
            {
                Console.WriteLine($"���� {chapterName}���� ���� ������ ����Ʈ�� �����ϴ�.");
            }
            Console.WriteLine("0. ���ư���");
        }

        public void DisplayAcceptMenu(int questIndex)
        {
                Console.Clear();
                Quest selectedQuest = questLists[questIndex - 1];
                QuestManager.Instance.ShowQuest(selectedQuest);

                Console.WriteLine("1. ����");
                Console.WriteLine("2. ����");
                string input = Console.ReadLine() ?? "";

                if (input == "1")
                {
                    QuestManager.Instance.AcceptQuest(selectedQuest);
                    Console.WriteLine("����Ʈ�� �����߽��ϴ�.");
                    Console.ReadLine();
                }
                else if (input == "2")
                {
                    Console.WriteLine("����Ʈ�� �����߽��ϴ�.");
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
            Console.WriteLine("���͸� ���� ����ϼ���");
            Console.ReadLine();
        }

        public void DisplayRewardMenu(int questIndex)
        {
            Console.Clear();
            Quest selectedQuest = questLists[questIndex - 1];
            QuestManager.Instance.ShowQuest(selectedQuest);

            Console.WriteLine("1. ���� �ޱ�");
            Console.WriteLine("2. ���ƿ���");
            string input = Console.ReadLine() ?? "";

            if (input == "1")
            {
                Console.WriteLine("������ ȹ���߽��ϴ�.");
                QuestManager.Instance.Quests.Remove(selectedQuest); ;
                Console.ReadLine();
            }
            else if (input == "2")
            {
            }
        }
    }
}