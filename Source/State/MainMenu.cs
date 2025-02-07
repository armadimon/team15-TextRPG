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
            Console.WriteLine("\n2. ����");
            Console.WriteLine("\n3. hn����");

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
                    gameManager.ChangeState(new CombatState());
                    break;
                case "3":
                    gameManager.ChangeState(new JHNCombatState());
                    break;
                case "0":
                    gameManager.QuitGame();
                    break;
            }
        }
    }
}