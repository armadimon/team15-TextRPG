using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.State
{
    internal class CombatState : IGameState
    {

        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("\n1. 승리");
            Console.WriteLine("\n2. 전투");

            Console.WriteLine("0. 나가기");
        }

        public void HandleInput()
        {
            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
            string input = Console.ReadLine() ?? "";

            switch (input)
            {
                case "1":
                    GameManager.Instance.GameData.Player.Health--;
                    Console.WriteLine($"{GameManager.Instance.GameData.Player.Health}");
                    GameManager.Instance.ChangeState(new BattleMenuState());
                    break;
                case "2":
                    //GameManager.Instance.ChangeState(new JHNCombatState());
                    break;
                case "0":
                    GameManager.Instance.ChangeState(new MainMenuState());
                    break;
            }
        }

        public void BattleScene()
        {

        }
    }
}
