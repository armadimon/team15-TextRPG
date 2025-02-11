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
        public NPC Npc { get; set; }
        public JHNCombat combat;
        public JHNCombatPhysical combatPhy;

        public JHNCombatState(NPC npc)
        {
            combat = new JHNCombat();
            combatPhy = new JHNCombatPhysical();

            Npc = npc;
        }

        public void DisplayMenu(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("[해킹 시작]");
            Console.WriteLine("");
            
            Console.Write($"현재까지 얻어낸 정보: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Npc.RevealedName}");
            Console.ResetColor();
            Console.WriteLine("");

            Console.WriteLine("1. 정보 스캔");
            Console.WriteLine("2. 해킹 공격");
            Console.WriteLine("3. 주먹 공격");
            Console.WriteLine("0. 도망");
        }

        public void HandleInput(GameManager gameManager)
        {
            if (Npc.Health <= 0)
            {
                gameManager.ChangeState(new ExploreState(gameManager.GameData.CurrentChapter.CurrentStage.Name));
                return;
            }
            string input = Console.ReadLine() ?? "";
            switch (input)
            {
                case "1":
                    combat.ScanEnemy(gameManager.GameData.Player, Npc);
                    break;
                case "2":
                    combat.Hack(gameManager.GameData.Player, gameManager.GameData.CurrentChapter, Npc);
                    break;
                case "3":
                    combatPhy.Attack(gameManager.GameData.Player, Npc);
                    break;
                case "0":
                    gameManager.ChangeState(new MainMenuState());
                    return;
            }
        }


    }

}
