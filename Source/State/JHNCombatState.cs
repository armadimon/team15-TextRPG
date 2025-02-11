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

        public void DisplayMenu()
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

        public void HandleInput()
        {
            if (Npc.Health <= 0)
            {
                GameManager.Instance.ChangeState(new ExploreState(GameManager.Instance.GameData.CurrentChapter.CurrentStage.Name));
                return;
            }
            string input = Console.ReadLine() ?? "";
            switch (input)
            {
                case "1":
                    combat.ScanEnemy(gameManager.GameData.Player, Npc);
                    break;
                case "2":
                    combat.Hack(GameManager.Instance.GameData.Player, GameManager.Instance.GameData.CurrentChapter, Npc);
                    break;
                case "3":
                    combatPhy.Attack(GameManager.Instance.GameData.Player, Npc);
                    break;
                case "0":
                    GameManager.Instance.ChangeState(new MainMenuState());
                    return;
            }
        }


    }

}
