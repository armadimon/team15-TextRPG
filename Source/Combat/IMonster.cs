using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.Combat
{
    public interface IMonster
    {
        string Type { get; set; }
        string MonsterName { get; set; }
        int Level { get; set; }
        double Health { get; set; }
        string SkillName { get; set; }
        int AttackDamage { get; set; }
        int SkillDamage { get; set; }
        double ArmorRisistence { get; set; }
        double SkillRisistence { get; set; }

        void Attak(Player player)
        {
            Console.WriteLine($"{MonsterName}(이/가) 공격했다.");
            player.Health -= AttackDamage;
        }

        void UseSkill(Player player)
        {
            Console.WriteLine($"{MonsterName}(이/가) {SkillName}(을/를) 사용했다.");
            player.Health -= SkillDamage;
        }

    }

    public class Robo : IMonster
    {
        public string Type { get; set; } = "robo";
        public string MonsterName { get; set; } = "경비로봇";
        public int Level { get; set; } = 1;
        public double Health { get; set; } = 100;
        public string SkillName { get; set; } = "몽둥이질";
        public int AttackDamage { get; set; } = 2;
        public int SkillDamage { get; set; } = 5;
        public double ArmorRisistence { get; set; } = 0;
        public double SkillRisistence { get; set; } = 0;

        public void Attak(Player player)
        {
            Console.WriteLine($"{MonsterName}(이/가) 공격했다.");
            player.Health -= AttackDamage;
        }

        public void UseSkill(Player player)
        {
            Console.WriteLine($"{MonsterName}(이/가) {SkillName}(을/를) 사용했다.");
            player.Health -= SkillDamage;
        }
    }

    public class Cybo : IMonster
    {
        public string Type { get; set; } = "cybo";
        public string MonsterName { get; set; } = "경비팀장";
        public int Level { get; set; } = 2;
        public double Health { get; set; } = 125;
        public string SkillName { get; set; } = "몽둥이질";
        public int AttackDamage { get; set; } = 3;
        public int SkillDamage { get; set; } = 5;
        public double ArmorRisistence { get; set; } = 1;
        public double SkillRisistence { get; set; } = 1;

        public void Attak(Player player)
        {
            Console.WriteLine($"{MonsterName}(이/가) 공격했다.");
            player.Health -= AttackDamage;
        }

        public void UseSkill(Player player)
        {
            Console.WriteLine($"{MonsterName}(이/가) {SkillName}(을/를) 사용했다.");
            player.Health -= SkillDamage;
        }
    }

    public class Human : IMonster
    {
        public string Type { get; set; } = "human";
        public string MonsterName { get; set; } = "경찰";
        public int Level { get; set; } = 3;
        public double Health { get; set; } = 150;
        public string SkillName { get; set; } = "스턴건";
        public int AttackDamage { get; set; } = 3;
        public int SkillDamage { get; set; } = 7;
        public double ArmorRisistence { get; set; } = 2;
        public double SkillRisistence { get; set; } = 2;

        public void Attak(Player player)
        {
            Console.WriteLine($"{MonsterName}(이/가) 공격했다.");
            player.Health -= AttackDamage;
        }

        public void UseSkill(Player player)
        {
            Console.WriteLine($"{MonsterName}(이/가) {SkillName}(을/를) 사용했다.");
            player.Health -= SkillDamage;
        }
    }



}
