using _15TextRPG.Source.Combat;
using System;
using System.Numerics;
using System.Reflection.Metadata;

namespace _15TextRPG.Source
{
    //[Serializable]
    public interface IItem 
    { 
        // Tag 1 = AttackPoint, Tag 2 = DeffencePoint
        public int Tag { get; }
        public int Stat { get; }
        public int Value { get; }
        public string Name { get; }
        public string Desc { get; }

        public void Use(ICharacter? target = null);
    }

    class Example() : IItem
    {
        public int Tag { get; } = 0;

        public int Stat { get; } = 0;

        public int Value { get; } = 0;

        public string Name { get; } = "Example";

        public string Desc { get; } = string.Empty;

        public void Use(ICharacter? target)
        {
            if(target is not null)
            {
                if (target is Player)
                {
                    Player player = (Player)target;
                    Console.WriteLine(player.Name);
                }
                else if (target is IMonster)
                {
                    IMonster monster = (IMonster)target;
                    Console.WriteLine(monster.MonsterName);
                }
            }
        }
    }

    class HackTool() : IItem
    {
        public int Tag { get; } = 0;

        public int Stat { get; } = 0;

        public int Value { get; } = 0;

        public string Name { get; } = "HackTool";

        public string Desc { get; } = string.Empty;

        public void Use(ICharacter? target)
        {
            if (target is not null)
            {
                if (target is Player)
                {
                    Player player = (Player)target;
                    Console.WriteLine(player.Name);
                }
                else if (target is IMonster)
                {
                    IMonster monster = (IMonster)target;
                    Console.WriteLine(monster.MonsterName);
                }
            }
        }
    }

    class RecoveryItem : IItem
    {
        public int Tag { get; }
        public int Stat { get; }
        public int Value { get; }
        public string Name { get; }
        public string Desc { get; }
        public int RecoveryAmount { get; }
      
        private Player Player { get; }

        public RecoveryItem(Player player)
        {
            Player = player;
        }
        public RecoveryItem(int tag, int stat, int value, string name, string desc, int recoveryAmount)
        {
            Tag = tag;
            Stat = stat;
            Value = value;
            Name = name;
            Desc = desc;
            RecoveryAmount = recoveryAmount;

            Player = GameManager.Instance.GameData.Player;
        }

        public void Use(ICharacter? target)
        {
            Player.Health = Math.Min(Player.Health + RecoveryAmount, Player.MaxHP);
            Console.WriteLine($"{RecoveryAmount}만큼 플레이어 회복함 -> {Player.Health}");
        }

        public void Use(Player player)
        {
            Player.Health = Math.Min(Player.Health + RecoveryAmount, Player.MaxHP);
            Console.WriteLine($"{RecoveryAmount}만큼 플레이어 회복함 -> {Player.Health}");
        }

    }
}
