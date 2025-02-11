using System;

namespace _15TextRPG.Source.State
{
    public class MainMenuState : IGameState
    {
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("\n[캠프 입구]");
            Console.WriteLine("\n1. 상태 보기");
            Console.WriteLine("\n2. 의뢰 수주");
            Console.WriteLine("\n3. 챕터 선택");
            Console.WriteLine("\n4. hn전투");
            Console.WriteLine("\n5. JW전투");
            Console.WriteLine("0. 종료");
        }

        public void HandleInput()
        {
            GameManager.Instance.GameData.CurrentChapter = null;
            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
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