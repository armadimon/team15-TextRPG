using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source.Combat;

namespace _15TextRPG.Source.State
{
    public class BattleMenuState : IGameState
    {

        public void DisplayMenu(GameManager gameManager)
        {
            Console.Clear();
            Random random = new Random();
            int num = random.Next(1, 4);
            for(int i = 0; i < num; i++)
            {
                gameManager.BattleManager.SpawnMonster(new Robo(), 0, 9);
            }

            for (int i = 0; i < gameManager.BattleManager.monsters.Count; i++)
            {
                while (gameManager.BattleManager.monsters[i].Health != 0)
                {
                    Console.Clear();
                    gameManager.BattleManager.BattleStat(gameManager.Player);
                    gameManager.BattleManager.ShowMonster(0, 9);
                    Console.SetCursorPosition(0, 13);
                    Console.Write("1. 공격  ");
                    Console.Write("2. 스킬  ");
                    Console.WriteLine("3. 방어");
                    Console.Write("\n원하시는 행동을 입력해주세요. >> ");
                    string input = Console.ReadLine() ?? "";
                    switch (input)
                    {
                        case "1":
                            AtkPhase(gameManager);
                            break;
                        case "2":
                            SkillPhase(gameManager);
                            break;
                        case "3":

                            break;

                        case "4":
                            break;
                    }
                }
            }
            Console.Clear();
            gameManager.BattleManager.BattleStat(gameManager.Player);
            gameManager.BattleManager.ShowMonster(0, 9);
            Console.WriteLine();
            Console.WriteLine("전투에서 승리했습니다.");
            Console.ReadLine();
        }

        public void AtkPhase(GameManager gameManager)
        {
            ReChoose:
            Console.Clear();
            gameManager.BattleManager.BattleStat(gameManager.Player);
            gameManager.BattleManager.ShowMonster(0, 9);

            Console.Write("\n원하시는 대상을 입력해주세요. >> ");
            string input = Console.ReadLine() ?? "";
            switch (input)
            {
                case "1":
                    gameManager.Player.Attack(gameManager, 0);
                    break;
                case "2":
                    if (gameManager.BattleManager.monsters[1] != null)
                        gameManager.Player.Attack(gameManager, 1);
                    else
                        goto ReChoose;
                    break;
                case "3":
                    if (gameManager.BattleManager.monsters[2] != null)
                        gameManager.Player.Attack(gameManager, 2);
                    else
                        goto ReChoose;
                    break;
                case "4":
                    if (gameManager.BattleManager.monsters[3] != null)
                        gameManager.Player.Attack(gameManager, 3);
                    else
                        goto ReChoose;
                    break;
            }
        }

        public void SkillPhase(GameManager gameManager)
        {
        ReChoose:
            Console.Clear();
            gameManager.BattleManager.skills.Add(new RailGun());
            gameManager.BattleManager.ShowSkill();
            gameManager.BattleManager.ShowMonster(0, 9);

            Console.Write("\n원하시는 스킬을 입력해주세요. >> ");
            int i = int.Parse(Console.ReadLine() ?? "");
            Console.Write("\n원하시는 대상을 입력해주세요. >> ");
            string input = Console.ReadLine() ?? "";

            switch (input)
            {
                case "1":
                    gameManager.Player.UseSkill(gameManager, 0, gameManager.BattleManager.skills[i - 1]);
                    break;
                case "2":
                    if (gameManager.BattleManager.monsters[1] != null)
                        gameManager.Player.UseSkill(gameManager, 1, gameManager.BattleManager.skills[i - 1]);
                    else
                        goto ReChoose;
                    break;
                case "3":
                    if (gameManager.BattleManager.monsters[2] != null)
                        gameManager.Player.UseSkill(gameManager, 2, gameManager.BattleManager.skills[i - 1]);
                    else
                        goto ReChoose;
                    break;
                case "4":
                    if (gameManager.BattleManager.monsters[3] != null)
                        gameManager.Player.UseSkill(gameManager, 3, gameManager.BattleManager.skills[i - 1]);
                    else
                        goto ReChoose;
                    break;
            }
        }

        public void DefPhase()
        {

        }

        public void HandleInput(GameManager gameManager)
        {
            gameManager.ChangeState(new ExploreState("stage1"));
        }        
    }
}