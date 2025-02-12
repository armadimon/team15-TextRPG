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
            Console.Write("\n���Ͻô� �ൿ�� �Է����ּ���. >> ");
            string input = Console.ReadLine() ?? "";
            switch (input)
            {
                case "1":
                    if (GameManager.Instance.GameData.Player.StatPoint != 0)
                        GameManager.Instance.BattleManager.StatPhase();
                    else
                        Console.WriteLine("StatPoint�� �����ϴ�.");
                        Thread.Sleep(750);
                    break;
                case "0":
                    GameManager.Instance.ChangeState(new MainMenuState());
                    break;
            }
        }
    }
}