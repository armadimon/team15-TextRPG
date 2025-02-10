using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    public class Player : IInventoryOwner
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double AttackDamage { get; set; }
        public double SkillDamage { get; set; }
        public int DefensePoint { get; set; }
        public int SkillDefensePoint { get; set; }
        public int MaxHP { get; set; }
        public int Health { get; set; }
        public int MaxMP { get; set; }
        public int MP { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int ClearCount { get; set; }
        public Item? Weapon { get; set; }
        public Item? Armor { get; set; }

        public Inventory Inventory { get; set; }

        public Player(string name)
        {
            Name = name;
            Level = 1;
            Description = "용병";
            AttackDamage = 10;
            SkillDamage = 20;
            DefensePoint = 5;
            MaxHP = 100;
            Health = MaxHP;
            MaxMP = 50;
            MP = MaxMP;
            Gold = 1500;
            Inventory = new(this);
        }

        public void ShowStatus()
        {

            string ap = Weapon != null
                ? $"{Weapon.Stat:+#;-#;0}"
                : "0";

            string dp = Armor != null
                ? $"{Armor.Stat:+#;-#;0}"
                : "0";

            Console.WriteLine($"상태 보기");
            Console.WriteLine($"캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine($"Lv . {Level:D2}");
            Console.WriteLine($"{Name} ({Description})");
            Console.WriteLine($"공격력 : {AttackDamage} ({ap})");
            Console.WriteLine($"방어력 : {DefensePoint} ({dp})");
            Console.WriteLine($"체력 : {Health}");
            Console.WriteLine($"Gold : {Gold} G");
        }

        public void Attack(GameManager gameManager, int i)
        {
            Console.WriteLine($"{gameManager.BattleManager.monsters[i].MonsterName}(을/를) 공격합니다");
            Console.ReadLine();
            gameManager.BattleManager.monsters[i].Health -= gameManager.Player.AttackDamage + gameManager.BattleManager.monsters[i].ArmorRisistence;
            if (gameManager.BattleManager.monsters[i].Health < 0)
            {
                gameManager.BattleManager.monsters[i].Health = 0;
            }
        }

        public void UseSkill(GameManager gameManager, int i, ISKill skill, IMonster monster)
        {
            Console.WriteLine($"{gameManager.BattleManager.monsters[i].MonsterName}에게 {skill.SkillName}(을/를) 사용합니다");
            Console.ReadLine();
            if(skill.SkillType == monster.Type)
            {
                gameManager.BattleManager.monsters[i].Health -= (gameManager.Player.SkillDamage + skill.BonusDamage) - gameManager.BattleManager.monsters[i].SkillRisistence;
            }
            else
            {
                gameManager.BattleManager.monsters[i].Health -= gameManager.Player.SkillDamage - gameManager.BattleManager.monsters[i].SkillRisistence;
            }

            gameManager.Player.MP -= skill.SkillCost;
            if (gameManager.BattleManager.monsters[i].Health < 0)
            {
                gameManager.BattleManager.monsters[i].Health = 0;
            }
        }
    }
}
