using System;
using _15TextRPG.Source.hn;

namespace _15TextRPG.Source.State
{
    public class MainMenuState : IGameState
    {
        public void DisplayMenu(GameManager gameManager)
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

        public void HandleInput(GameManager gameManager)
        {
            Console.Write("\n���Ͻô� �ൿ�� �Է����ּ���. >> ");
            string input = Console.ReadLine() ?? "";

            switch (input)
            {
                case "1":
                    gameManager.ChangeState(new StatusMenuState());
                    break;
                case "2":
                    gameManager.ChangeState(new ContractState());
                    break;
                case "3":
                    gameManager.ChangeState(new ChapterState());
                    break;
                case "4":
                    gameManager.ChangeState(new JHNCombatState());
                    break;
                case "5":
                    gameManager.ChangeState(new BattleMenuState());
                    break;
                case "0":
                    gameManager.QuitGame();
                    break;
            }
        }
    }
}