using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using _15TextRPG.Source;
using _15TextRPG;
using _15TextRPG.Source.State;

namespace _15TextRPG.Source
{
    public class BattleManager
    {
        bool defensePose = false;
        bool lose = false;
        public List<IMonster> monsters = new List<IMonster>();
        public List<ISKill> skills = new List<ISKill>();

        public void SpawnMonster(GameManager gameManager)
        {
            Random random = new Random();
            int num = random.Next(1, 4);

            for (int i = 0; i < num; i++)
            {
                int percent = random.Next(0, 100);
                if (percent < 50)
                    monsters.Add(new Robo());
                else if (percent < 75)
                    monsters.Add(new Cybo());
                else
                    monsters.Add(new Human());
            }
        }

        public void ShowMonster(bool num, int x, int y)
        {
            if(num)
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].Health <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.SetCursorPosition(x + i * 15, y);
                        Console.WriteLine($"{i + 1} " + monsters[i].MonsterName);
                        Console.SetCursorPosition(x + i * 15, y + 1);
                        Console.WriteLine("Lv. " + monsters[i].Level);
                        Console.SetCursorPosition(x + i * 15, y + 2);
                        Console.WriteLine("Dead");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.SetCursorPosition(x + i * 15, y);
                        Console.WriteLine($"{i + 1} " + monsters[i].MonsterName);
                        Console.SetCursorPosition(x + i * 15, y + 1);
                        Console.WriteLine("Lv. " + monsters[i].Level);
                        Console.SetCursorPosition(x + i * 15, y + 2);
                        Console.WriteLine("HP " + monsters[i].Health);
                    }
                }
            }
            else
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].Health <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.SetCursorPosition(x + i * 15, y);
                        Console.WriteLine(monsters[i].MonsterName);
                        Console.SetCursorPosition(x + i * 15, y + 1);
                        Console.WriteLine("Lv. " + monsters[i].Level);
                        Console.SetCursorPosition(x + i * 15, y + 2);
                        Console.WriteLine("Dead");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.SetCursorPosition(x + i * 15, y);
                        Console.WriteLine(monsters[i].MonsterName);
                        Console.SetCursorPosition(x + i * 15, y + 1);
                        Console.WriteLine("Lv. " + monsters[i].Level);
                        Console.SetCursorPosition(x + i * 15, y + 2);
                        Console.WriteLine("HP " + monsters[i].Health);
                    }
                }
            }
        }

        public void BattleStat(Player player)
        {

            string ap = player.Weapon != null
                ? $"{player.Weapon.Stat:+#;-#;0}"
                : "0";

            string dp = player.Armor != null
                ? $"{player.Armor.Stat:+#;-#;0}"
                : "0";

            Console.SetCursorPosition(60, 0);
            Console.WriteLine($"Stat");
            Console.SetCursorPosition(60, 1);
            Console.WriteLine($"{player.Name} ({player.Description})");
            Console.SetCursorPosition(60, 2);
            Console.WriteLine($"Lv . {player.Level}");
            Console.SetCursorPosition(60, 3);
            Console.WriteLine($"Exp. {player.Exp} / {player.MaxExp}");
            Console.SetCursorPosition(60, 4);
            Console.WriteLine($"공격력 : {player.AttackDamage} ({ap})");
            Console.SetCursorPosition(60, 5);
            Console.WriteLine($"스킬 공격력 : {player.SkillDamage}");
            Console.SetCursorPosition(60, 6);
            Console.WriteLine($"방어력 : {player.DefensePoint} ({dp})");
            Console.SetCursorPosition(60, 7);
            Console.WriteLine($"스킬방어력 : {player.SkillDefensePoint}");
            Console.SetCursorPosition(60, 8);
            Console.WriteLine($"체력 : {player.Health}");
            Console.SetCursorPosition(60, 9);
            Console.WriteLine($"MP : {player.MP}");
            Console.SetCursorPosition(60, 10);
            Console.WriteLine($"Gold : {player.Gold} G");
        }

        public void AddSkill(ISKill skill)
        {
            skills.Add(skill);
        }

        public void ShowSkill()
        {
            Console.SetCursorPosition(60, 0);
            Console.WriteLine("Skills");

            for ( int i = 0; i < skills.Count; i++)
            {
                Console.SetCursorPosition(60, (i + 1) * 2);
                Console.WriteLine($"{i + 1} {skills[0].SkillName}: {skills[0].Description}");
            }
        }
        public void InBattle(GameManager gameManager)
        {
            skills.Add(new RailGun()); //임시 스킬
            Console.Clear();
            bool Runable = false;
            gameManager.BattleManager.SpawnMonster(gameManager);
            gameManager.BattleManager.ShowMonster(false, 0, 9);

            for (int i = 0; i < gameManager.BattleManager.monsters.Count; i++)
            {
                while (gameManager.BattleManager.monsters[i].Health != 0)
                {
                ReChoose:
                    Console.Clear();
                    gameManager.BattleManager.BattleStat(gameManager.Player);
                    gameManager.BattleManager.ShowMonster(false, 0, 9);
                    Console.SetCursorPosition(0, 13);
                    Console.Write("1. 공격  ");
                    Console.Write("2. 스킬  ");
                    Console.Write("3. 방어  ");
                    Console.WriteLine("4. 전략적 후퇴");
                    Console.Write("\n원하시는 행동을 입력해주세요. >> ");
                    string input = Console.ReadLine() ?? "";
                    int j;
                    if (!int.TryParse(input, out j) || j > 4)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        goto ReChoose;
                    }
                    else
                    {
                        switch (input)
                        {
                            case "1":
                                AtkPhase(gameManager);
                                break;
                            case "2":
                                SkillPhase(gameManager);
                                break;
                            case "3":
                                DefPhase(gameManager);
                                break;
                            case "4":
                                Runable = true;
                                goto Runable;
                            default:
                                goto ReChoose;
                        }
                    }

                    int deathCount = 0;
                    for (int k = 0; k < monsters.Count; k++)
                    {
                        if (monsters[k].Health <= 0)
                        {
                            deathCount++;
                        }
                    }

                    if (deathCount == monsters.Count)
                    {
                        goto Victory;
                    }

                    MonsterPhase(gameManager, gameManager.BattleManager);
                    if (lose)
                        goto BattleLose;
                   
                }
            }
        Victory:
        Runable:
            if (Runable)
            {
                Console.WriteLine("전투에서 후퇴했습니다");
                Console.ReadLine();
            }
            else
            {   
                Console.Clear();
                gameManager.BattleManager.BattleStat(gameManager.Player);
                gameManager.BattleManager.ShowMonster(false, 0, 9);
                Console.WriteLine();
                Console.WriteLine("전투에서 승리했습니다.");
                Console.ReadLine();
            }

            if(!Runable)
            {
                for (int i = 0; i < gameManager.BattleManager.monsters.Count; i++)
                {
                    gameManager.Player.Gold += (int)Math.Truncate(gameManager.BattleManager.monsters[i].MaxHealth) * 10;
                }
            }

        BattleLose:
            gameManager.BattleManager.monsters.Clear();
            gameManager.Player.Health = gameManager.Player.MaxHP;
            gameManager.Player.MP = gameManager.Player.MaxMP;


        }

        public void AtkPhase(GameManager gameManager)
        {
        ReChoose:
            Console.Clear();
            gameManager.BattleManager.BattleStat(gameManager.Player);
            gameManager.BattleManager.ShowMonster(true, 0, 9);

            Console.Write("\n원하시는 대상을 입력해주세요. >> ");
            string input = Console.ReadLine() ?? "";
            int j;
            if (!int.TryParse(input, out j) || j > gameManager.BattleManager.monsters.Count || gameManager.BattleManager.monsters[j-1].Health <= 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadLine();
                goto ReChoose;
            }
            else
            {
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
        }

        public void SkillPhase(GameManager gameManager)
        {
        ReChooseSkill:
            Console.Clear();
            gameManager.BattleManager.ShowSkill();
            gameManager.BattleManager.ShowMonster(true, 0, 9);

            Console.Write("\n원하시는 스킬을 입력해주세요. >> ");
            string input1 = Console.ReadLine() ?? "";
            int i = int.Parse(input1);
            if (!int.TryParse(input1, out i) || i > gameManager.BattleManager.skills.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadLine();
                goto ReChooseSkill;
            }
            else
            {
            ReChooseTarget:
                Console.Clear();
                gameManager.BattleManager.ShowSkill();
                gameManager.BattleManager.ShowMonster(true, 0, 9);
                Console.Write("\n원하시는 대상을 입력해주세요. >> ");
                string input2 = Console.ReadLine() ?? "";
                int j;
                if (!int.TryParse(input2, out j) || j > gameManager.BattleManager.monsters.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    goto ReChooseTarget;
                }
                switch (input2)
                {
                    case "1":
                        gameManager.Player.UseSkill(gameManager, 0, gameManager.BattleManager.skills[i - 1], gameManager.BattleManager.monsters[i - 1]);
                        break;
                    case "2":
                        if (gameManager.BattleManager.monsters[1] == null)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            goto ReChooseTarget;
                        }
                        else
                            gameManager.Player.UseSkill(gameManager, 1, gameManager.BattleManager.skills[i - 1], gameManager.BattleManager.monsters[i - 1]);
                        break;
                    case "3":
                        if (gameManager.BattleManager.monsters[2] == null)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            goto ReChooseTarget;
                        }
                        else
                            gameManager.Player.UseSkill(gameManager, 2, gameManager.BattleManager.skills[i - 1], gameManager.BattleManager.monsters[i - 1]);
                        break;
                    case "4":
                        if (gameManager.BattleManager.monsters[3] == null)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            goto ReChooseTarget;
                        }
                        else
                            gameManager.Player.UseSkill(gameManager, 3, gameManager.BattleManager.skills[i - 1], gameManager.BattleManager.monsters[i - 1]);
                        break;
                }
            }
        }

        public void DefPhase(GameManager gameManager)
        {
            gameManager.BattleManager.defensePose = true;
            Console.WriteLine("방어자세를 취하여 정신력을 회복합니다.");
            gameManager.Player.MP += 10;
            if (gameManager.Player.MP >= gameManager.Player.MaxMP)
                gameManager.Player.MP = gameManager.Player.MaxMP;
            Console.ReadLine();
        }

        public void MonsterPhase(GameManager gameManager, BattleManager battleManager)
        {
            Console.Clear();
            gameManager.BattleManager.BattleStat(gameManager.Player);
            gameManager.BattleManager.ShowMonster(false, 0, 9);
            Console.WriteLine();
            Console.WriteLine("적의 공격이 시작됩니다.");
            Console.ReadLine();

            for (int i = 0; i < battleManager.monsters.Count; i++)
            {
                if (battleManager.monsters[i].Health > 0)
                {
                    Console.Clear();
                    gameManager.BattleManager.BattleStat(gameManager.Player);
                    gameManager.BattleManager.ShowMonster(false, 0, 9);
                    Console.WriteLine();
                    MonsterAttack(gameManager, i);
                    Console.Clear();
                    gameManager.BattleManager.BattleStat(gameManager.Player);
                    gameManager.BattleManager.ShowMonster(false, 0, 9);

                    if (gameManager.Player.Health <= 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("적의 공격으로 쓰러졌습니다. 강제 귀환됩니다.");
                        Console.ReadLine();
                        gameManager.BattleManager.lose = true;
                        break;
                    }
                }
            }

            if(!lose)
            {          
                Console.WriteLine();
                Console.WriteLine("적의 공격이 끝났습니다.");
                Console.ReadLine();
                gameManager.BattleManager.defensePose = false;
            }
        }

        public void MonsterAttack(GameManager gameManager, int i)
        {
            Random random = new Random();
            int j = random.Next(0, 100);
            if (gameManager.BattleManager.monsters[i].Type == "robo")
            {
                if (j < 15)
                {
                    gameManager.BattleManager.monsters[i].UseSkill(gameManager.Player);

                    if (gameManager.BattleManager.defensePose == true && gameManager.BattleManager.monsters[i].SkillDamage > gameManager.Player.DefensePoint)
                        gameManager.Player.Health += gameManager.Player.DefensePoint;
                    else if (gameManager.BattleManager.defensePose == true && gameManager.BattleManager.monsters[i].SkillDamage <= gameManager.Player.DefensePoint)
                        gameManager.Player.Health += gameManager.BattleManager.monsters[i].SkillDamage;
                }
                else
                {
                    gameManager.BattleManager.monsters[i].Attack(gameManager.Player);

                    if (gameManager.BattleManager.defensePose == true && gameManager.BattleManager.monsters[i].AttackDamage > gameManager.Player.DefensePoint)
                        gameManager.Player.Health += gameManager.Player.DefensePoint;
                    else if (gameManager.BattleManager.defensePose == true && gameManager.BattleManager.monsters[i].AttackDamage <= gameManager.Player.DefensePoint)
                        gameManager.Player.Health += gameManager.BattleManager.monsters[i].AttackDamage;
                }

            }
            else if (gameManager.BattleManager.monsters[i].Type == "cybo")
            {
                if (j < 30)
                {
                    gameManager.BattleManager.monsters[i].UseSkill(gameManager.Player);

                    if (gameManager.BattleManager.defensePose == true && gameManager.BattleManager.monsters[i].SkillDamage > gameManager.Player.DefensePoint)
                        gameManager.Player.Health += gameManager.Player.DefensePoint;
                    else if (gameManager.BattleManager.defensePose == true && gameManager.BattleManager.monsters[i].SkillDamage <= gameManager.Player.DefensePoint)
                        gameManager.Player.Health += gameManager.BattleManager.monsters[i].SkillDamage;
                }
                else
                {
                    gameManager.BattleManager.monsters[i].Attack(gameManager.Player);

                    if (gameManager.BattleManager.defensePose == true && gameManager.BattleManager.monsters[i].AttackDamage > gameManager.Player.DefensePoint)
                        gameManager.Player.Health += gameManager.Player.DefensePoint;
                    else if (gameManager.BattleManager.defensePose == true && gameManager.BattleManager.monsters[i].AttackDamage <= gameManager.Player.DefensePoint)
                        gameManager.Player.Health += gameManager.BattleManager.monsters[i].AttackDamage;
                }

            }
            else
            {
                if (j < 45)
                {
                    gameManager.BattleManager.monsters[i].UseSkill(gameManager.Player);

                    if (gameManager.BattleManager.defensePose == true && gameManager.BattleManager.monsters[i].SkillDamage > gameManager.Player.DefensePoint)
                        gameManager.Player.Health += gameManager.Player.DefensePoint;
                    else if (gameManager.BattleManager.defensePose == true && gameManager.BattleManager.monsters[i].SkillDamage <= gameManager.Player.DefensePoint)
                        gameManager.Player.Health += gameManager.BattleManager.monsters[i].SkillDamage;
                }
                else
                {
                    gameManager.BattleManager.monsters[i].Attack(gameManager.Player);

                    if (gameManager.BattleManager.defensePose == true && gameManager.BattleManager.monsters[i].AttackDamage > gameManager.Player.DefensePoint)
                        gameManager.Player.Health += gameManager.Player.DefensePoint;
                    else if (gameManager.BattleManager.defensePose == true && gameManager.BattleManager.monsters[i].AttackDamage <= gameManager.Player.DefensePoint)
                        gameManager.Player.Health += gameManager.BattleManager.monsters[i].AttackDamage;
                }

            }

            if (gameManager.Player.Health <= 0)
                gameManager.Player.Health = 0;
        }
    }    
}
