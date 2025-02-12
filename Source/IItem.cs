using System;
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

        public void Use();
    }

    class Example() : IItem
    {
        public int Tag { get; } = 0;

        public int Stat { get; } = 0;

        public int Value { get; } = 0;

        public string Name { get; } = "Example";

        public string Desc { get; } = string.Empty;

        public void Use()
        {
            Console.WriteLine("Use Example Item!");
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
        }
        public void Use()
        {
            Player.Health = Math.Min(Player.Health + RecoveryAmount, Player.MaxHP);
            
        }

    }
}
