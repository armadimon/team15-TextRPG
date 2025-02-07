using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.State
{
    public class BattleMenuState : IGameState
    {
        public BattleMenuState(GameManager gameManager)
        {
            gameManager.MonsterSpawn(4);
        }
        public void DisplayMenu(GameManager gameManager)
        {
            //stage a - b의 정보를 통해 적의 수와 레벨을 결정(a, b는 GameManager에?)
            Console.Clear();
            for (int i = 0; i < 4; i ++)
            {
                gameManager.Enemy.EnemyStat(i, 0 + i * 20);
            }
            gameManager.Player.BattleStat();
            Console.SetCursorPosition(0, 14);
            Console.Write("1. 공격  ");
            Console.Write("2. 스킬  ");
            Console.Write("3. 방어  ");
            Console.WriteLine("4. 아이템");
        }

        public void HandleInput(GameManager gameManager, string input)
        {
            switch (input)
            {
                case "1":
                    gameManager.AtkPhase();
                    break;
                //case 2:

                //case 3:

                //case 4:
                default:
                    
                    break;
            }
        }
    }
}