//using _15TextRPG.Source.State;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace _15TextRPG
//{
//    public class GameManager
//    {
//        private static GameManager? instance;
//        private IGameState currentState;
//        private bool isRunning = true;
//        public Player Player { get; private set; }

//        public GameManager()
//        {
//            Player = new Player("Default");
//            currentState = new MainMenuState();
//        }

//        public static GameManager Instance // 싱글톤
//        {
//            get
//            {
//                if (instance == null)
//                    instance = new GameManager();
//                return instance;
//            }
//        }

//        public void Run()
//        {
//            while (isRunning)
//            {
//                currentState.DisplayMenu(this);
//                Console.Write("\n원하시는 행동을 입력해주세요. >> ");
//                string input = Console.ReadLine() ?? "";
//                currentState.HandleInput(this, input);
//            }
//        }

//        public void ChangeState(IGameState newState)
//        {
//            currentState = newState;
//        }

//        public void QuitGame()
//        {
//            Console.WriteLine("게임을 종료합니다.");
//            isRunning = false;
//        }
//    }
//}
