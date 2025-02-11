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
        public int Tag => throw new NotImplementedException();

        public int Stat => throw new NotImplementedException();

        public int Value => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string Desc => throw new NotImplementedException();

        public void Use()
        {
            throw new NotImplementedException();
        }
    }
}
