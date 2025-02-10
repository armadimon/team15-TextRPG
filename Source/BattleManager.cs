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

namespace _15TextRPG.Source
{
    public class BattleManager
    {
        public bool defensePose = false;
        public List<IMonster> monsters = new List<IMonster>();
        public List<ISKill> skills = new List<ISKill>();
        public void SpawnMonster(IMonster monster, int x, int y)
        {
                monsters.Add(monster);
        }

        public void ShowMonster(bool num, int x, int y)
        {
            if(num)
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    Console.SetCursorPosition(x + i * 15, y);
                    Console.WriteLine($"{i + 1} " + monsters[i].MonsterName);
                    Console.SetCursorPosition(x + i * 15, y + 1);
                    Console.WriteLine("Lv. " + monsters[i].Level);
                    Console.SetCursorPosition(x + i * 15, y + 2);
                    Console.WriteLine("HP " + monsters[i].Health);
                }
            }
            else
            {
                for (int i = 0; i < monsters.Count; i++)
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
            Console.WriteLine($"공격력 : {player.AttackDamage} ({ap})");
            Console.SetCursorPosition(60, 4);
            Console.WriteLine($"스킬 공격력 : {player.SkillDamage}");
            Console.SetCursorPosition(60, 5);
            Console.WriteLine($"방어력 : {player.DefensePoint} ({dp})");
            Console.SetCursorPosition(60, 6);
            Console.WriteLine($"스킬방어력 : {player.SkillDefensePoint}");
            Console.SetCursorPosition(60, 7);
            Console.WriteLine($"체력 : {player.Health}");
            Console.SetCursorPosition(60, 8);
            Console.WriteLine($"MP : {player.MP}");
            Console.SetCursorPosition(60, 9);
            Console.WriteLine($"Gold : {player.Gold} G");
        }

        public void AddSkill(ISKill skill)
        {
            skills.Add(skill);
        }

        public void ShowSkill()
        {
            for ( int i = 0; i < skills.Count; i += 3)
            {
                Console.SetCursorPosition(60, i);
                Console.WriteLine($"Skills");
                Console.SetCursorPosition(60, i + 1);
                Console.WriteLine($"{i + 1} {skills[0].SkillName}: {skills[0].Description}");
                Console.SetCursorPosition(60, 2);
            }
        }
    }
}
