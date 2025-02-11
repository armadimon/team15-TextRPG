using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source.Combat;

namespace _15TextRPG.Source.State
{
    public class JHNCombatState : IGameState
    {

        public JHNCombat combat;
        public JHNCombatState()
        {
            combat = new JHNCombat();
        }

        public void DisplayMenu(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("[해킹 시작]");
            //Console.WriteLine($"{gamedata.Player.Name} vs  {combat.GetRevealedEnemyName()} ");
            //Console.WriteLine($"{gamedata.Player.Name} - HP: {gamedata.Player.Health}");
            //Console.WriteLine($"{combat.GetRevealedEnemyName()} - HP: {combat.Enemy.Health}");
            Console.WriteLine("");
            Console.WriteLine("1. 정보 스캔");
            Console.WriteLine("2. 해킹 공격");
            Console.WriteLine("3. 주먹 공격");

            Console.WriteLine("0. 도망");
        }

        public void HandleInput(GameManager gameManager)
        {
            string input = Console.ReadLine() ?? "";
            switch (input)
            {
                case "1":
                    combat.ScanEnemy(gameManager.GameData.enemies[0]);
                    break;
                case "2":
                    combat.Hack(gameManager.GameData.Player, gameManager.GameData.enemies[0]);
                    break;
                case "3":
                    combat.Attack(gameManager.GameData.Player, gameManager.GameData.enemies[0]);
                    break;
                case "0":
                    gameManager.ChangeState(new MainMenuState());
                    return;
            }
        }


    }

}
