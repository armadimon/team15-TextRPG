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
}
