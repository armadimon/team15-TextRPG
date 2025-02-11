using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
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
        string Description { get; set; }

        
        event Action<IMonster> OnDeath; // 죽을 때 발생하는 이벤트
        List<IItem> Items { get; }  // 몬스터가 가지고 있는 아이템 목록

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
        Reward GetReward();


    }

    public class Robo : IMonster
    {
        public string Type { get; set; } = "robo";
        public string MonsterName { get; set; } = "경비로봇";
        public int Level { get; set; } = 1;
        private double health = 100;
        public double Health
        {
            get => health;
            set
            {
                health = value;
                if (health <= 0) // 체력이 0 이하가 되면 OnDeath 이벤트 발생
                {
                    health = 0;
                    OnDeath?.Invoke(this);
                }
            }
        }
        public string SkillName { get; set; } = "몽둥이질";
        public int AttackDamage { get; set; } = 2;
        public int SkillDamage { get; set; } = 5;
        public double ArmorRisistence { get; set; } = 0;
        public double SkillRisistence { get; set; } = 0;
        public string Description { get; set; } = "robo";

        public event Action<IMonster> OnDeath;

        

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
        private void CheckHealth()
        {
            if (Health <= 0)
            {
                Console.WriteLine($"{MonsterName}이(가) 쓰러졌다.");
                OnDeath?.Invoke(this);
            }
        }

        public List<IItem> Items { get; } = new List<IItem>
        {
            new OtherItems(1, 5, "로봇 부품", () => Console.WriteLine("경비로봇의 부품이다."))
        };
        public Reward GetReward()
        {
            return new Reward(10, Items);
        }

    }

    public class Cybo : IMonster
    {
        public string Type { get; set; } = "cybo";
        public string MonsterName { get; set; } = "경비팀장";
        public int Level { get; set; } = 2;
        private double health = 125;
        public double Health
        {
            get => health;
            set
            {
                health = value;
                if (health <= 0) // 체력이 0 이하가 되면 OnDeath 이벤트 발생
                {
                    health = 0;
                    OnDeath?.Invoke(this);
                }
            }
        }
        public string SkillName { get; set; } = "몽둥이질";
        public int AttackDamage { get; set; } = 3;
        public int SkillDamage { get; set; } = 5;
        public double ArmorRisistence { get; set; } = 1;
        public double SkillRisistence { get; set; } = 1;
        public string Description { get; set; } = "cybo";

        public event Action<IMonster> OnDeath;

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

        private void CheckHealth()
        {
            if (Health <= 0)
            {
                Console.WriteLine($"{MonsterName}이(가) 쓰러졌다.");
                OnDeath?.Invoke(this);
            }
        }
        public List<IItem> Items { get; } = new List<IItem>
        {
            new OtherItems(1, 5, "로봇 부품", () => Console.WriteLine("경비로봇의 부품이다."))
        };
        public Reward GetReward()
        {
            return new Reward(10, Items);
        }
    }

    public class Human : IMonster
    {
        public string Type { get; set; } = "human";
        public string MonsterName { get; set; } = "경찰";
        public int Level { get; set; } = 3;
        private double health;
        public double Health
        {
            get => health;
            set
            {
                health = value;
                if (health <= 0) // 체력이 0 이하가 되면 OnDeath 이벤트 발생
                {
                    health = 0;
                    OnDeath?.Invoke(this);
                }
            }
        }
        public string SkillName { get; set; } = "스턴건";
        public int AttackDamage { get; set; } = 3;
        public int SkillDamage { get; set; } = 7;
        public double ArmorRisistence { get; set; } = 2;
        public double SkillRisistence { get; set; } = 2;
        public string Description { get; set; } = "human";

        public event Action<IMonster> OnDeath;

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
        private void CheckHealth()
        {
            if (Health <= 0)
            {
                Console.WriteLine($"{MonsterName}이(가) 쓰러졌다.");
                OnDeath?.Invoke(this);
            }
        }
        public List<IItem> Items { get; } = new List<IItem>
        {
            new OtherItems(1, 1, "로봇 부품", () => Console.WriteLine("경비로봇의 부품이다."))
        };
        public Reward GetReward()
        {
            return new Reward(10, Items);
        }
    }


    public class Hana : IMonster
    {
        public string Type { get; set; } = "human";
        public string MonsterName { get; set; } = "하나";
        public int Level { get; set; } = 3;
        private double health;
        public double Health
        {
            get => health;
            set
            {
                health = value;
                if (health <= 0)
                {
                    health = 0;
                    OnDeath?.Invoke(this);
                }
            }
        }
        public string SkillName { get; set; } = "버그 공격";
        public int AttackDamage { get; set; } = 3;
        public int SkillDamage { get; set; } = 7;
        public double ArmorRisistence { get; set; } = 2;
        public double SkillRisistence { get; set; } = 2;
        public string Description { get; set; } = "CYBER_HACK_9xTpL";

        public event Action<IMonster> OnDeath;

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
        private void CheckHealth()
        {
            if (Health <= 0)
            {
                Console.WriteLine($"{MonsterName}이(가) 쓰러졌다.");
                OnDeath?.Invoke(this);
            }
        }


        public List<IItem> Items { get; } = new List<IItem>
        {
            new OtherItems(2, 1, "남은 버그 덩어리", () => Console.WriteLine("하나가 남긴 버그다. 만지면 오류가 날 것 같다."))
        };
        public Reward GetReward()
        {
            return new Reward(10, Items);
        }
    }

}
