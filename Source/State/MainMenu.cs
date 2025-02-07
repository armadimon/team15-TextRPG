using System;
using _15TextRPG.Source.hn;

namespace _15TextRPG.Source.State
{
    public class MainMenuState : IGameState
    {
        public void DisplayMenu(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("\n[캠프 입구]");
            Console.WriteLine("\n1. 상태 보기");
            Console.WriteLine("\n2. 전투");
            Console.WriteLine("\n3. hn전투");

            Console.WriteLine("0. 종료");
        }

        public void HandleInput(GameManager gameManager)
        {
            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
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