using System;

namespace _15TextRPG.Source.State
{
    public class MainMenuState : IGameState
    {
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("\n[ķ�� �Ա�]");
            Console.WriteLine("\n1. ���� ����");
            Console.WriteLine("\n2. �Ƿ� ����");
            Console.WriteLine("\n3. é�� ����");
            Console.WriteLine("\n4. hn����");
            Console.WriteLine("\n5. JW����");
            Console.WriteLine("0. ����");
        }

        public void HandleInput()
        {
            GameManager.Instance.GameData.CurrentChapter = null;
            Console.Write("\n���Ͻô� �ൿ�� �Է����ּ���. >> ");
            string input = Console.ReadLine() ?? "";

            switch (input)
            {
                case "1":
                    GameManager.Instance.ChangeState(new StatusMenuState());
                    break;
                case "2":
                    GameManager.Instance.ChangeState(new ContractState());
                    break;
                case "3":
                    GameManager.Instance.ChangeState(new ChapterState());
                    break;
                case "4":
                    //GameManager.Instance.ChangeState(new JHNCombatState());
                    break;
                case "5":
                    GameManager.Instance.ChangeState(new BattleMenuState());
                    break;
                case "0":
                    GameManager.Instance.QuitGame();
                    break;
            }
        }
    }
}