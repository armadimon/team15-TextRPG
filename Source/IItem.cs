using System;
using System.Reflection.Metadata;

namespace _15TextRPG.Source
{
    //[Serializable]
    public abstract class IItem(int tag, int value, string name, Action _action)
    {
        // Tag 1 = AttackPoint, Tag 2 = DeffencePoint
        public int Tag { get; } = tag;
        public int Stat { get; }
        public int Value { get; } = value;
        public string Name { get; } = name;
        public string Desc { get; } = string.Empty;
        public Action action = _action;

        public void Use()
        {
            action();
        }

        public override string ToString()
        {
            return $"{Name}: {Tag}";
        }
    }

    class ICEBreaker : IItem
    {
        public ICEBreaker(int tag, int value, string name, Action _action) : base(tag, value, name, _action)
        {
        }
    }
}
