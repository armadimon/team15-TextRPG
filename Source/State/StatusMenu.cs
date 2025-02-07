using System;

namespace _15TextRPG.Source.State
{
    public class StatusMenuState : IGameState
    {
        public void DisplayMenu(GameManager gameManager)
        {
            Console.Clear();
            gameManager.GameData.Player.ShowStatus();
        }

        public void HandleInput(GameManager gameManager)
        {
            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
            string input = Console.ReadLine() ?? "";
            switch (input)
            {
                case "0":
                    gameManager.ChangeState(new MainMenuState());
                    break;
            }
        }
    }
}