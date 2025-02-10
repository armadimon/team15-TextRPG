using System;
using System.Reflection.Metadata;

namespace _15TextRPG.Source
{
    //[Serializable]
    public abstract class IItem(Action _action)
    {
        // Tag 1 = AttackPoint, Tag 2 = DeffencePoint
        public int Tag { get; }
        public int Stat { get; }
        public int Value { get; }
        public string Name { get; } = string.Empty;
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
        public ICEBreaker(Action _action) : base(_action)
        {
        }
    }
}
