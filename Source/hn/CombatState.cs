using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source.State;

namespace _15TextRPG.Source.hn
{
    public class CombatState : IGameState
    {

        private Combat combat;

        public CombatState()
        {
            combat = new Combat(GameManager.Instance.Player, EnemyManager.Instance.GetRandomEnemy());
        }

        public void DisplayMenu(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("[해킹 시작]");
            Console.WriteLine($"{combat.Player.Name} vs  {combat.GetRevealedEnemyName()} ");
            Console.WriteLine($"{combat.Player.Name} - HP: {combat.Player.Health}");
            Console.WriteLine($"{combat.GetRevealedEnemyName()} - HP: {combat.Enemy.Health}");
            Console.WriteLine("");
            Console.WriteLine("1. 정보 스캔");
            Console.WriteLine("2. 해킹 공격");
            Console.WriteLine("3. 주먹 공격");

            Console.WriteLine("0. 도망");
        }

        public void HandleInput(GameManager gameManager, string input)
        {
            switch (input)
            {
                case "1":
                    combat.ScanEnemy();
                    break;
                case "2":
                    combat.Hack();
                    break;
                case "3":
                    combat.Attack();
                    break;
                case "0":
                    combat.Run();
                    gameManager.ChangeState(new MainMenuState());
                    return;
            }
        }


    }

}
