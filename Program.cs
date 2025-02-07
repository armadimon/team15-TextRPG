using System;
using _15TextRPG.Source;
using _15TextRPG.Source.State;

namespace _15TextRPG
{
    class TextRPG
    {
        private static void Main()
        {
            GameManager gameManager = new GameManager();

            gameManager.Run();
        }
    }

    public class GameManager
    {
        private IGameState currentState;
        private bool isRunning = true;
        public GameData GameData { get; private set; }

        public GameManager()
        {
            GameData = new GameData();
            currentState = new MainMenuState();
        }

        public void Run()
        {
            while (isRunning)
            {
                currentState.DisplayMenu(this);
                currentState.HandleInput(this);
            }
        }

        public void ChangeState(IGameState newState)
        {
            currentState = newState;
        }

        public void QuitGame()
        {
            Console.WriteLine("게임을 종료합니다.");
            isRunning = false;
        }
    }
}
