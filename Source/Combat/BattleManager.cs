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

namespace _15TextRPG.Source.Combat
{
    public class BattleManager
    {
        bool defensePose = false;
        private int battleType = 0;
        bool lose = false;
        public List<IMonster> monsters = new List<IMonster>();
        public List<ISKill> skills = new List<ISKill>();
        public List<ISKill> userskills = new List<ISKill>();

        public void SpawnMonster()
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
            Console.WriteLine($"{player.Name} ({GameData.JobDescriptions[player.Job]})");
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
            Console.WriteLine($"치명타 확률 : {player.Critical} %");
            Console.SetCursorPosition(60, 11);
            Console.WriteLine($"회피율 : {player.Dodge} %");

        }

        public void AddSkill(GameManager gameManager)
        {
            for (int i = 0; i < skills.Count; i++)
            {
                if (gameManager.GameData.Player.Str >= skills[i].StrNeeded && gameManager.GameData.Player.Dex >= skills[i].DexNeeded)
                {
                    userskills.Add(skills[i]);
                    skills.RemoveAt(i);
                }
            }
        }
        public void SkillList(GameManager gameManager)
        {
            gameManager.BattleManager.skills.Add(new HandGun(gameManager));
            gameManager.BattleManager.skills.Add(new Raifle(gameManager));
            gameManager.BattleManager.skills.Add(new ShotGun(gameManager));
            gameManager.BattleManager.skills.Add(new MachineGun(gameManager));
            gameManager.BattleManager.skills.Add(new OverHeat(gameManager));
            gameManager.BattleManager.skills.Add(new ShortCircuit(gameManager));
            gameManager.BattleManager.skills.Add(new Cyberpsychosis(gameManager));
            gameManager.BattleManager.skills.Add(new Suicide(gameManager));
            gameManager.BattleManager.skills.Add(new SystemCollapse(gameManager));
        }

        public void ShowSkill()
        {
            Console.SetCursorPosition(60, 0);
            Console.WriteLine("특수공격");

            for ( int i = 0; i < userskills.Count; i++)
            {
                Console.SetCursorPosition(60, i * 3 + 2);
                Console.WriteLine($"{i + 1} {userskills[i].SkillName}");
                Console.SetCursorPosition(60, i * 3 + 3);
                Console.WriteLine($"{userskills[i].Description}");
            }
        }
        public void InBattle()
        {
            SkillList(gameManager);
            AddSkill(gameManager);
            Console.Clear();
            bool Runable = false;
            GameManager.Instance.BattleManager.SpawnMonster();
            GameManager.Instance.BattleManager.ShowMonster(false, 0, 9);

            for (int i = 0; i < GameManager.Instance.BattleManager.monsters.Count; i++)
            {
                while (GameManager.Instance.BattleManager.monsters[i].Health != 0)
                {
                ReChoose:
                    Console.Clear();
                    GameManager.Instance.BattleManager.BattleStat(GameManager.Instance.GameData.Player);
                    GameManager.Instance.BattleManager.ShowMonster(false, 0, 9);
                    Console.SetCursorPosition(0, 13);
                    Console.Write("1. 공격  ");
                    Console.Write("2. 특수공격  ");
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
                                AtkPhase();
                                break;
                            case "2":
                                SkillPhase();
                                break;
                            case "3":
                                DefPhase();
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

                    MonsterPhase(GameManager.Instance.BattleManager);
                    if (lose)
                        goto BattleLose;
                   
                }
            }
        Victory:
        Runable:
            if (Runable)
            {
                Console.WriteLine("전투에서 후퇴했습니다");
                Thread.Sleep(1500);

                if (GameManager.Instance.GameData.CurrentChapter == null)
                {
                    GameManager.Instance.ChangeState(new MainMenuState());
                }
                else
                {
                    GameManager.Instance.ChangeState(new ExploreState(GameManager.Instance.GameData.CurrentChapter.CurrentStage.Name));
                }
            }
            else
            {   
                Console.Clear();
                GameManager.Instance.BattleManager.BattleStat(GameManager.Instance.GameData.Player);
                GameManager.Instance.BattleManager.ShowMonster(false, 0, 9);
                Console.WriteLine();
                Console.WriteLine("전투에서 승리했습니다.");
                Thread.Sleep(1500);

                if (GameManager.Instance.GameData.CurrentChapter == null)
                {
                    GameManager.Instance.ChangeState(new MainMenuState());
                }
                else
                {
                    GameManager.Instance.ChangeState(new ExploreState(GameManager.Instance.GameData.CurrentChapter.CurrentStage.Name));
                }
            }

            if(!Runable)
            {
                for (int i = 0; i < GameManager.Instance.BattleManager.monsters.Count; i++)
                {
                    GameManager.Instance.GameData.Player.Gold += (int)Math.Truncate(GameManager.Instance.BattleManager.monsters[i].MaxHealth) * 10;
                }
            }

        BattleLose:
            GameManager.Instance.BattleManager.monsters.Clear();
            GameManager.Instance.GameData.Player.Health = GameManager.Instance.GameData.Player.MaxHP;
            GameManager.Instance.GameData.Player.MP = GameManager.Instance.GameData.Player.MaxMP;
            if (GameManager.Instance.GameData.CurrentChapter == null)
            {
                GameManager.Instance.ChangeState(new MainMenuState());
            }
            else
            {
                GameManager.Instance.ChangeState(new ExploreState(GameManager.Instance.GameData.CurrentChapter.CurrentStage.Name));
            }
        }

        public void AtkPhase()
        {
        ReChoose:
            Console.Clear();
            GameManager.Instance.BattleManager.BattleStat(GameManager.Instance.GameData.Player);
            GameManager.Instance.BattleManager.ShowMonster(true, 0, 9);

            Console.Write("\n원하시는 대상을 입력해주세요. >> ");
            string input = Console.ReadLine() ?? "";
            int j;
            if (!int.TryParse(input, out j) || j > GameManager.Instance.BattleManager.monsters.Count || GameManager.Instance.BattleManager.monsters[j-1].Health <= 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1500);
                goto ReChoose;
            }
            else
            {
                switch (input)
                {
                    case "1":
                        GameManager.Instance.GameData.Player.Attack(GameManager.Instance, 0);
                        break;
                    case "2":
                        if (GameManager.Instance.BattleManager.monsters[1] != null)
                            GameManager.Instance.GameData.Player.Attack(GameManager.Instance, 1);
                        else
                            goto ReChoose;
                        break;
                    case "3":
                        if (GameManager.Instance.BattleManager.monsters[2] != null)
                            GameManager.Instance.GameData.Player.Attack(GameManager.Instance, 2);
                        else
                            goto ReChoose;
                        break;
                    case "4":
                        if (GameManager.Instance.BattleManager.monsters[3] != null)
                            GameManager.Instance.GameData.Player.Attack(GameManager.Instance, 3);
                        else
                            goto ReChoose;
                        break;
                }
            }
        }

        public void SkillPhase()
        {
        ReChooseSkill:
            Console.Clear();
            GameManager.Instance.BattleManager.ShowSkill();
            GameManager.Instance.BattleManager.ShowMonster(true, 0, 9);

            Console.Write("\n원하시는 스킬을 입력해주세요. >> ");
            string input1 = Console.ReadLine() ?? "";
            int i = int.Parse(input1);

            if (!int.TryParse(input1, out i) || i > GameManager.Instance.BattleManager.skills.Count || i <= 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1500);
                goto ReChooseSkill;
            }
            else
            {
            ReChooseTarget:
                Console.Clear();
                GameManager.Instance.BattleManager.ShowSkill();
                GameManager.Instance.BattleManager.ShowMonster(true, 0, 9);
                Console.Write("\n원하시는 대상을 입력해주세요. >> ");
                string input2 = Console.ReadLine() ?? "";
                int j;

                if (!int.TryParse(input2, out j) || j > GameManager.Instance.BattleManager.monsters.Count || j <= 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    goto ReChooseTarget;
                }
                switch (input2)
                {
                    case "1":
                        GameManager.Instance.GameData.Player.UseSkill(GameManager.Instance, 0, GameManager.Instance.BattleManager.skills[i - 1], GameManager.Instance.BattleManager.monsters[i - 1]);
                      break;
                    case "2":
                        if (GameManager.Instance.BattleManager.monsters[1] == null)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            goto ReChooseTarget;
                        }
                        else
                            GameManager.Instance.GameData.Player.UseSkill(GameManager.Instance, 1, GameManager.Instance.BattleManager.skills[i - 1], GameManager.Instance.BattleManager.monsters[i - 1]);
                        break;
                    case "3":
                        if (GameManager.Instance.BattleManager.monsters[2] == null)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            goto ReChooseTarget;
                        }
                        else
                            GameManager.Instance.GameData.Player.UseSkill(GameManager.Instance, 2, GameManager.Instance.BattleManager.skills[i - 1], GameManager.Instance.BattleManager.monsters[i - 1]);
                        break;
                    case "4":
                        if (GameManager.Instance.BattleManager.monsters[3] == null)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            goto ReChooseTarget;
                        }
                        else
                            GameManager.Instance.GameData.Player.UseSkill(GameManager.Instance, 3, GameManager.Instance.BattleManager.skills[i - 1], GameManager.Instance.BattleManager.monsters[i - 1]);
                        break;
                }
            }
        }

        public void DefPhase()
        {
            GameManager.Instance.BattleManager.defensePose = true;
            Console.WriteLine("방어자세를 취하여 정신력을 회복합니다.");
            GameManager.Instance.GameData.Player.MP += 10;
            if (GameManager.Instance.GameData.Player.MP >= GameManager.Instance.GameData.Player.MaxMP)
                GameManager.Instance.GameData.Player.MP = GameManager.Instance.GameData.Player.MaxMP;
            Thread.Sleep(1500);
        }

        public void MonsterPhase(BattleManager battleManager)
        {
            Console.Clear();
            GameManager.Instance.BattleManager.BattleStat(GameManager.Instance.GameData.Player);
            GameManager.Instance.BattleManager.ShowMonster(false, 0, 9);
            Console.WriteLine();
            Console.WriteLine("적의 공격이 시작됩니다.");
            Thread.Sleep(1500);

            for (int i = 0; i < battleManager.monsters.Count; i++)
            {
                if (battleManager.monsters[i].Health > 0)
                {
                    Console.Clear();
                    GameManager.Instance.BattleManager.BattleStat(GameManager.Instance.GameData.Player);
                    GameManager.Instance.BattleManager.ShowMonster(false, 0, 9);
                    Console.WriteLine();
                    MonsterAttack(i);
                    Console.Clear();
                    GameManager.Instance.BattleManager.BattleStat(GameManager.Instance.GameData.Player);
                    GameManager.Instance.BattleManager.ShowMonster(false, 0, 9);

                    if (GameManager.Instance.GameData.Player.Health <= 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("적의 공격으로 쓰러졌습니다. 강제 귀환됩니다.");
                        Thread.Sleep(1500);
                        GameManager.Instance.BattleManager.lose = true;
                        break;
                    }
                }
            }

            if(!lose)
            {          
                Console.WriteLine();
                Console.WriteLine("적의 공격이 끝났습니다.");
                Thread.Sleep(1500);
                GameManager.Instance.BattleManager.defensePose = false;
            }
        }

        public void MonsterAttack(int i)
        {
            Random random = new Random();
            int j = random.Next(0, 100);
            int k = random.Next(0, 100);
            if (GameManager.Instance.BattleManager.monsters[i].Type == "robo")
            {
                if (j < 15)
                {
                    GameManager.Instance.BattleManager.monsters[i].UseSkill(GameManager.Instance.GameData.Player);

                    if (k <= GameManager.Instance.GameData.Player.Dodge)
                    {
                        Console.WriteLine($"{GameManager.Instance.BattleManager.monsters[i].MonsterName}의 {GameManager.Instance.BattleManager.monsters[i].SkillName}(을/를) 피했습니다.");
                        Thread.Sleep(1500);
                        GameManager.Instance.GameData.Player.Health += GameManager.Instance.BattleManager.monsters[i].SkillDamage;
                    }
                    else
                    {
                        if (GameManager.Instance.BattleManager.defensePose == true && GameManager.Instance.BattleManager.monsters[i].SkillDamage > GameManager.Instance.GameData.Player.SkillDefensePoint)
                            GameManager.Instance.GameData.Player.Health += GameManager.Instance.GameData.Player.SkillDefensePoint;
                        else if (GameManager.Instance.BattleManager.defensePose == true && GameManager.Instance.BattleManager.monsters[i].SkillDamage <= GameManager.Instance.GameData.Player.SkillDefensePoint)
                            GameManager.Instance.GameData.Player.Health += GameManager.Instance.BattleManager.monsters[i].SkillDamage;
                    }

                }
                else
                {
                    GameManager.Instance.BattleManager.monsters[i].Attack(GameManager.Instance.GameData.Player);

                    if (k <= GameManager.Instance.GameData.Player.Dodge)
                    {
                        Console.WriteLine($"{GameManager.Instance.BattleManager.monsters[i].MonsterName}의 공격을 피했습니다.");
                        Thread.Sleep(1500);
                        GameManager.Instance.GameData.Player.Health += GameManager.Instance.BattleManager.monsters[i].AttackDamage;
                    }
                    else
                    {
                        if (GameManager.Instance.BattleManager.defensePose == true && GameManager.Instance.BattleManager.monsters[i].AttackDamage > GameManager.Instance.GameData.Player.DefensePoint)
                            GameManager.Instance.GameData.Player.Health += GameManager.Instance.GameData.Player.DefensePoint;
                        else if (GameManager.Instance.BattleManager.defensePose == true && GameManager.Instance.BattleManager.monsters[i].AttackDamage <= GameManager.Instance.GameData.Player.DefensePoint)
                            GameManager.Instance.GameData.Player.Health += GameManager.Instance.BattleManager.monsters[i].AttackDamage;
                    }
                }

            }
            else if (GameManager.Instance.BattleManager.monsters[i].Type == "cybo")
            {
                if (j < 30)
                {
                    GameManager.Instance.BattleManager.monsters[i].UseSkill(GameManager.Instance.GameData.Player);

                    if (k <= GameManager.Instance.GameData.Player.Dodge)
                    {
                        Console.WriteLine($"{GameManager.Instance.BattleManager.monsters[i].MonsterName}의 {GameManager.Instance.BattleManager.monsters[i].SkillName}(을/를) 피했습니다.");
                        Thread.Sleep(1500);
                        GameManager.Instance.GameData.Player.Health += GameManager.Instance.BattleManager.monsters[i].SkillDamage;
                    }
                    else
                    {
                        if (GameManager.Instance.BattleManager.defensePose == true && GameManager.Instance.BattleManager.monsters[i].SkillDamage > GameManager.Instance.GameData.Player.SkillDefensePoint)
                            GameManager.Instance.GameData.Player.Health += GameManager.Instance.GameData.Player.SkillDefensePoint;
                        else if (GameManager.Instance.BattleManager.defensePose == true && GameManager.Instance.BattleManager.monsters[i].SkillDamage <= GameManager.Instance.GameData.Player.SkillDefensePoint)
                            GameManager.Instance.GameData.Player.Health += GameManager.Instance.BattleManager.monsters[i].SkillDamage;
                    }

                }
                else
                {
                    GameManager.Instance.BattleManager.monsters[i].Attack(GameManager.Instance.GameData.Player);

                    if (k <= GameManager.Instance.GameData.Player.Dodge)
                    {
                        Console.WriteLine($"{GameManager.Instance.BattleManager.monsters[i].MonsterName}의 공격을 피했습니다.");
                        Thread.Sleep(1500);
                        GameManager.Instance.GameData.Player.Health += GameManager.Instance.BattleManager.monsters[i].AttackDamage;
                    }
                    else
                    {
                        if (GameManager.Instance.BattleManager.defensePose == true && GameManager.Instance.BattleManager.monsters[i].AttackDamage > GameManager.Instance.GameData.Player.DefensePoint)
                            GameManager.Instance.GameData.Player.Health += GameManager.Instance.GameData.Player.DefensePoint;
                        else if (GameManager.Instance.BattleManager.defensePose == true && GameManager.Instance.BattleManager.monsters[i].AttackDamage <= GameManager.Instance.GameData.Player.DefensePoint)
                            GameManager.Instance.GameData.Player.Health += GameManager.Instance.BattleManager.monsters[i].AttackDamage;
                    }
                }

            }
            else
            {
                if (j < 45)
                {
                    GameManager.Instance.BattleManager.monsters[i].UseSkill(GameManager.Instance.GameData.Player);

                    if (k <= GameManager.Instance.GameData.Player.Dodge)
                    {
                        Console.WriteLine($"{GameManager.Instance.BattleManager.monsters[i].MonsterName}의 {GameManager.Instance.BattleManager.monsters[i].SkillName}(을/를) 피했습니다.");
                        Thread.Sleep(1500);
                        GameManager.Instance.GameData.Player.Health += GameManager.Instance.BattleManager.monsters[i].SkillDamage;
                    }
                    else
                    {
                        if (GameManager.Instance.BattleManager.defensePose == true && GameManager.Instance.BattleManager.monsters[i].SkillDamage > GameManager.Instance.GameData.Player.SkillDefensePoint)
                            GameManager.Instance.GameData.Player.Health += GameManager.Instance.GameData.Player.SkillDefensePoint;
                        else if (GameManager.Instance.BattleManager.defensePose == true && GameManager.Instance.BattleManager.monsters[i].SkillDamage <= GameManager.Instance.GameData.Player.SkillDefensePoint)
                            GameManager.Instance.GameData.Player.Health += GameManager.Instance.BattleManager.monsters[i].SkillDamage;
                    }

                }
                else
                {
                    GameManager.Instance.BattleManager.monsters[i].Attack(GameManager.Instance.GameData.Player);

                    if (k <= GameManager.Instance.GameData.Player.Dodge)
                    {
                        Console.WriteLine($"{GameManager.Instance.BattleManager.monsters[i].MonsterName}의 공격을 피했습니다.");
                        Thread.Sleep(1500);
                        GameManager.Instance.GameData.Player.Health += GameManager.Instance.BattleManager.monsters[i].AttackDamage;
                    }
                    else
                    {
                        if (GameManager.Instance.BattleManager.defensePose == true && GameManager.Instance.BattleManager.monsters[i].AttackDamage > GameManager.Instance.GameData.Player.DefensePoint)
                            GameManager.Instance.GameData.Player.Health += GameManager.Instance.GameData.Player.DefensePoint;
                        else if (GameManager.Instance.BattleManager.defensePose == true && GameManager.Instance.BattleManager.monsters[i].AttackDamage <= GameManager.Instance.GameData.Player.DefensePoint)
                            GameManager.Instance.GameData.Player.Health += GameManager.Instance.BattleManager.monsters[i].AttackDamage;
                    }
                }

            }

            if (GameManager.Instance.GameData.Player.Health <= 0)
                GameManager.Instance.GameData.Player.Health = 0;
        }
    }    
}
