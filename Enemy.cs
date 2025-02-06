using _15TextRPG.Source.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG
{
    public class Enemy
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double AttackDamage { get; set; }
        public int DefensePoint { get; set; }
        public int Health { get; set; } // 해킹 게이지로 사용
        public int Level { get; set; }


        public List<Item> DropItem; // 승리시 플레이어가 얻을 수 있는 아이템

        public Enemy(string name, int level, string description, double attackDamage, int defensePoint, int health, List<Item> item)
        {
            Name = name;
            Level = level;
            Description = description;
            AttackDamage = attackDamage;
            DefensePoint = defensePoint;
            Health = health;

            DropItem = new List<Item>();
        }

        // 복사 생성자
        public Enemy(Enemy original)
        {
            Name = original.Name;
            Level = original.Level;
            Description = original.Description;
            AttackDamage = original.AttackDamage;
            DefensePoint = original.DefensePoint;
            Health = original.Health;
            DropItem = new List<Item>(original.DropItem);
        }

    }
}
