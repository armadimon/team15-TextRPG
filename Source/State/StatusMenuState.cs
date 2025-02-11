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
                case "0":
                    GameManager.Instance.ChangeState(new MainMenuState());
                    break;
            }
        }
    }
}