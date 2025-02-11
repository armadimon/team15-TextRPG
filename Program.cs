using System;
using _15TextRPG.Source;
using _15TextRPG.Source.Combat;
using _15TextRPG.Source.State;

namespace _15TextRPG
{
    class TextRPG
    {
        private static void Main()
        {

            GameManager.Instance.Run();
        }
    }

    public class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameManager();
                return instance;
            }
        }

        private IGameState currentState;
        private bool isRunning = true;
        public GameData GameData { get; private set; }
        public BattleManager BattleManager { get; private set; }

        private GameManager()
        {
            GameData = new GameData();
            currentState = new TitleMenuState();
            BattleManager = new BattleManager();
        }

        public void Run()
        {
            while (isRunning)
            {
                currentState.DisplayMenu();
                currentState.HandleInput();
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

