using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _15TextRPG.Source.Combat;

namespace _15TextRPG.Source
{
    public class Player : IInventoryOwner
    {
        public string Name { get; set; }
        public Job Job { get; set; }
        public double AttackDamage { get; set; }
        public double SkillDamage { get; set; }
        public int Intelligence { get; set; }
        public int DefensePoint { get; set; }
        public int SkillDefensePoint { get; set; }
        public int Str { get; set; }
        public int Dex { get; set; }
        public int Critical { get; set; }
        public int Dodge { get; set; }
        public int MaxHP { get; set; }
        public int Health { get; set; }
        public int MaxMP { get; set; }
        public int MP { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int StatPoint { get; set; }
        public int Exp {  get; set; }
        public int MaxExp { get; set; }
        public int ClearCount { get; set; }
        public Item? Weapon { get; set; }
        public Item? Armor { get; set; }

        public Inventory Inventory { get; set; }



        public Player(string name, Job job)
        {
            Name = name;
            Level = 1;
            StatPoint = 0;
            Exp = 0;
            MaxExp = 5;
            Job = job;
            Str = 3;
            Dex = 1;
            Critical = 20 + Dex;
            Dodge = 15 + Dex;
            AttackDamage = 140 + Str;
            SkillDamage = 20 + Dex;
            DefensePoint = 5 + Str;
            SkillDefensePoint = 5 + Str;
            MaxHP = 100;
            Health = MaxHP;
            MaxMP = 25;
            MP = MaxMP;
            Gold = 1500;
            Inventory = new();
        }

        //public Player(string name, int description) // 민첩캐
        //{
        //    Name = name;
        //    Level = 1;
        //    SkillPoint = 0;
        //    Exp = 0;
        //    MaxExp = 5;
        //    Description = "부랑아";
        //    Str = 1;
        //    Dex = 3;
        //    Critical = 25 + Dex;
        //    Dodge = 20 + Dex;
        //    AttackDamage = 20 + Str;
        //    SkillDamage = 40 + Dex;
        //    DefensePoint = 5 + Str;
        //    SkillDefensePoint = 5 + Str;
        //    MaxHP = 100;
        //    Health = MaxHP;
        //    MaxMP = 50;
        //    MP = MaxMP;
        //    Gold = 1500;
        //    Inventory = new(this);
        //}

        //public Player(string name, int description) / 지능캐
        //{
        //    Name = name;
        //    Level = 1;
        //    SkillPoint = 0;
        //    Exp = 0;
        //    MaxExp = 5;
        //    Description = "기업";
        //    Str = 1;
        //    Dex = 1;
        //    Critical = 20 + Dex;
        //    Dodge = 15 + Dex;
        //    AttackDamage = 20 + Str;
        //    SkillDamage = 20 + Str;
        //    DefensePoint = 2 + Str;
        //    SkillDefensePoint = 2 + Str;
        //    MaxHP = 100;
        //    Health = MaxHP;
        //    MaxMP = 50;
        //    MP = MaxMP;
        //    Gold = 1500;
        //    Inventory = new (this);
        //}

        public void ShowStatus()
        {

            string ap = Weapon != null
                ? $"{Weapon.Stat:+#;-#;0}"
                : "0";

            string dp = Armor != null
                ? $"{Armor.Stat:+#;-#;0}"
                : "0";
            Console.WriteLine($"[상태 보기]");
            Console.WriteLine($"캐릭터의 정보가 표시됩니다.\n");

            string imagePath= imagePath = "..\\..\\..\\image\\logo1.bmp";
            int width = 40; // 출력할 너비
            if (GameData.JobDescriptions[Job] == GameData.JobDescriptions[Job.Nomad])
            {
                imagePath = "..\\..\\..\\image\\logo1.bmp";
                width = 40;
            }
            else if (GameData.JobDescriptions[Job] == GameData.JobDescriptions[Job.Gutterchild])
            {
                imagePath = "..\\..\\..\\image\\logo2.bmp";
                width = 50;
            }
            else
            {
                imagePath = "..\\..\\..\\image\\example.bmp";
                width = 50;
            }
            

            

            string ascii = AsciiArtRenderer.ConvertBmpToAscii(imagePath, width);

            AsciiArtRenderer.PrintAsciiArt(0, 0, ascii); // 아스키 아트 출력

            Console.WriteLine($"Lv . {Level:D2}");
            Console.WriteLine($"{Name} ({GameData.JobDescriptions[Job]})");
            Console.WriteLine($"공격력 : {AttackDamage} ({ap})");
            Console.WriteLine($"방어력 : {DefensePoint} ({dp})");
            Console.WriteLine($"체력 : {Health}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine($"Str : {Str}");
            Console.WriteLine($"Dex : {Dex}");
        }

        public void Attack(GameManager gameManager, int i)
        {
            Random random = new Random();
            int j = random.Next(90, 111);
            int k = random.Next(0, 100);
            Console.WriteLine($"{gameManager.BattleManager.monsters[i].MonsterName}(을/를) 공격합니다");
            Thread.Sleep(1500);

            if( k <= gameManager.GameData.Player.Critical)
            {
                Console.WriteLine($"{gameManager.BattleManager.monsters[i].MonsterName}에게 치명적인 피해를 입혔습니다.");
                Thread.Sleep(1500);
                gameManager.BattleManager.monsters[i].Health -= Math.Round(gameManager.GameData.Player.AttackDamage * j / 100) * 1.5 + gameManager.BattleManager.monsters[i].ArmorRisistence;
            }
            else
            {
                gameManager.BattleManager.monsters[i].Health -= Math.Round(gameManager.GameData.Player.AttackDamage * j / 100) + gameManager.BattleManager.monsters[i].ArmorRisistence;
            }            

            if (gameManager.BattleManager.monsters[i].Health <= 0)
            {
                gameManager.BattleManager.monsters[i].Health = 0;
                gameManager.GameData.Player.Exp += gameManager.BattleManager.monsters[i].Level;

                if (gameManager.GameData.Player.Exp >= gameManager.GameData.Player.MaxExp)
                {
                    gameManager.GameData.Player.Level += gameManager.GameData.Player.Exp / gameManager.GameData.Player.MaxExp;
                    gameManager.GameData.Player.StatPoint += gameManager.GameData.Player.Level;
                    gameManager.GameData.Player.Exp %= gameManager.GameData.Player.MaxExp;
                }

                Console.Clear();
                gameManager.BattleManager.BattleStat(gameManager.GameData.Player);
                gameManager.BattleManager.ShowMonster(false, 0, 9);
                Console.WriteLine();
                Console.WriteLine($"{gameManager.BattleManager.monsters[i].MonsterName}(이/가) 쓰러졌습니다.");

                Reward reward = gameManager.BattleManager.monsters[i].GetReward();
                if (reward.Items.Count > 0)
                {
                    Console.WriteLine("아이템 획득:");
                    foreach (var item in reward.Items)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"{item.Name}");
                        Console.ResetColor();
                        gameManager.GameData.Player.Inventory.Add(item);
                    }
                }
                //else
                //{
                //    Console.WriteLine("획득한 아이템 없음.");
                //}

                Thread.Sleep(1500);

            }
        }

        public void UseSkill(GameManager gameManager, int i, ISKill skill, IMonster monster)
        {
            Console.WriteLine($"{gameManager.BattleManager.monsters[i].MonsterName}에게 {skill.SkillName}(을/를) 사용합니다");
            Thread.Sleep(1500);
            if (skill.SkillType == monster.Type)
            {
                gameManager.BattleManager.monsters[i].Health -= (gameManager.GameData.Player.SkillDamage + skill.BonusDamage) - gameManager.BattleManager.monsters[i].SkillRisistence;
            }
            else
            {
                gameManager.BattleManager.monsters[i].Health -= gameManager.GameData.Player.SkillDamage - gameManager.BattleManager.monsters[i].SkillRisistence;
            }

            gameManager.GameData.Player.MP -= skill.SkillCost;

            if (gameManager.BattleManager.monsters[i].Health <= 0)
            {
                gameManager.BattleManager.monsters[i].Health = 0;
                gameManager.GameData.Player.Exp += gameManager.BattleManager.monsters[i].Level;

                if (gameManager.GameData.Player.Exp >= gameManager.GameData.Player.MaxExp)
                {
                    gameManager.GameData.Player.Level += gameManager.GameData.Player.Exp / gameManager.GameData.Player.MaxExp;
                    gameManager.GameData.Player.Exp %= gameManager.GameData.Player.MaxExp;
                }

                

                Console.Clear();
                gameManager.BattleManager.BattleStat(gameManager.GameData.Player);
                gameManager.BattleManager.ShowMonster(false, 0, 9);
                Console.WriteLine();
                Console.WriteLine($"{gameManager.BattleManager.monsters[i].MonsterName}(이/가) 쓰러졌습니다.");

                Reward reward = gameManager.BattleManager.monsters[i].GetReward();
                if (reward.Items.Count > 0)
                {
                    Console.WriteLine("아이템 획득:");
                    foreach (var item in reward.Items)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"{item.Name}");
                        Console.ResetColor();
                        gameManager.GameData.Player.Inventory.Add(item);
                    }
                }
                else
                {
                    Console.WriteLine("획득한 아이템 없음.");
                }

                Thread.Sleep(1500);
            }
        }
    }
}
