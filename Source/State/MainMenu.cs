using System;

namespace _15TextRPG.Source.State
{
    public class MainMenuState : IGameState
    {
        public void DisplayMenu(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("\n[캠프 입구]");
            Console.WriteLine("\n1. 상태 보기");
            Console.WriteLine("2. 전투 하기");

            Console.WriteLine("0. 종료");
        }

        public void HandleInput(GameManager gameManager, string input)
        {
            switch (input)
            {
                case "1":
                    gameManager.ChangeState(new StatusMenuState());
                    break;
                case "2":
                    gameManager.ChangeState(new CombatState());
                    break;
                case "0":
                    gameManager.QuitGame();
                    break;
            }
        }
    }
}