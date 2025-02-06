using System;

namespace _15TextRPG.Source.State
{
    //[Serializable]
    public class Item
    {
        // Tag 1 = AttackPoint, Tag 2 = DeffencePoint
        public int Tag { get; set; }
        public int Stat { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public Item(int tag, int stat, int value, string name, string desc)
        {
            Tag = tag;
            Stat = stat;
            Value = value;
            Name = name;
            Desc = desc;
        }
    }
}
