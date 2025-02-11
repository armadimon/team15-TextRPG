using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source.State;

namespace _15TextRPG.Source.hn
{
    public class JHNCombatState : IGameState
    {

        public JHNCombat combat;
        public JHNCombatState()
        {
            this.combat = new JHNCombat();
        }

        public void DisplayMenu(GameManager gameManager)
        {
            Console.Clear();
            IMonster currentEnemy = gameManager.GameData.monsters[0];   // 여기 그 챕터에 나올 몬스터로 받아오면됨.

            Console.WriteLine("[해킹 시작]");
            Console.WriteLine($"{gameManager.GameData.Player.Name} vs  {combat.GetRevealedEnemyName(currentEnemy)} ");
            Console.WriteLine($"{gameManager.GameData.Player.Name} - HP: {gameManager.GameData.Player.Health}");
            Console.WriteLine($"{combat.GetRevealedEnemyName(currentEnemy)} - HP: {currentEnemy.Health}");
            Console.WriteLine("");
            Console.WriteLine("1. 정보 스캔");
            Console.WriteLine("2. 해킹 공격");
            Console.WriteLine("3. 주먹 공격");

            Console.WriteLine("0. 도망");
        }

        public void HandleInput(GameManager gameManager)
        {
            string input = Console.ReadLine() ?? "";
            IMonster currentEnemy = gameManager.GameData.monsters[0];  // 여기 그 챕터에 나올 몬스터로 받아오면됨.
            switch (input)
            {
                case "1":
                    combat.ScanEnemy(currentEnemy);
                    break;
                case "2":
                    combat.Hack(gameManager.GameData.Player, currentEnemy);
                    break;
                case "3":
                    combat.Attack(gameManager.GameData.Player, currentEnemy);
                    break;
                case "0":
                    gameManager.ChangeState(new MainMenuState());
                    return;
            }
        }


    }

}
