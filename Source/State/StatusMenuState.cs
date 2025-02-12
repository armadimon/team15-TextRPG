using System;

namespace _15TextRPG.Source.State
{
    public class StatusMenuState : IGameState
    {
        public void DisplayMenu()
        {
            Console.Clear();
            GameManager.Instance.GameData.Player.ShowStatus();
        }

        public void HandleInput()
        {
            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
            string input = Console.ReadLine() ?? "";
            switch (input)
            {
                case "1":
                    if (GameManager.Instance.GameData.Player.StatPoint != 0)
                        GameManager.Instance.BattleManager.StatPhase();
                    else
                        Console.WriteLine("StatPoint가 없습니다.");
                        Thread.Sleep(750);
                    break;
                case "0":
                    GameManager.Instance.ChangeState(new MainMenuState());
                    break;
            }
        }
    }
}