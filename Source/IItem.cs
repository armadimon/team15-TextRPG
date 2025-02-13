using _15TextRPG.Source.Combat;
using _15TextRPG.Source.State;
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
        public int Tag { get; } = (int)ItemList.HpRecovery;
        public int Stat { get; } = 1;
        public int Value { get; } = 1;
        public string Name { get; } = "HP 포션";
        public string Desc { get; } = "HP를 50만큼 회복시켜주는 포션이다.";
        public int RecoveryAmount { get; } = 50;
      
        private Player Player { get; } = GameManager.Instance.GameData.Player;


        public void Use(ICharacter? target)
        {
            if (Player.Health == Player.MaxHP)
            {
                Console.WriteLine("플레이어는 이미 최대 체력입니다.");
                return;
            }
            Player.Health = Math.Min(Player.Health + RecoveryAmount, Player.MaxHP);
            Console.WriteLine($"{RecoveryAmount}만큼 플레이어 회복함 -> {Player.Health}");
        }
    }

    class StateUpgradeItem : IItem
    {
        public int Tag { get; } = (int)ItemList.StrUpPotion;
        public int Stat { get; } = 1;
        public int Value { get; } = 1;
        public string Name { get; } = "힘이나는 포션";
        public string Desc { get; } = "STR 수치를 1증가 시켜주는 포션";
        public int RecoveryAmount { get; } = 1;

        private Player Player { get; } = GameManager.Instance.GameData.Player;

        public void Use(ICharacter? target)
        {
            
            Console.WriteLine($"{RecoveryAmount}만큼 플레이어의 STR이 증가 -> {Player.Str}");
        }
    }
    
    class MiliwareIceBreaker : IItem
    {
        public int Tag { get; } = (int)ItemList.StrUpPotion;
        public int Stat { get; } = 1;
        public int Value { get; } = 1;
        public string Name { get; } = "밀리브레이커";
        public string Desc { get; } = "해킹시 보안 등급을 5단계 낮춥니다.";
        
        public void Use(ICharacter? target)
        {
            if (target is IHackable)
            {
                IHackable Target = (IHackable)target;
                if (Target.HackDefenderLV < 5)
                {
                    Target.HackDefenderLV = 0;
                }
            }

        }
    }
    class OthersItem : IItem
    {
        public int Tag { get; } = (int)ItemList.Others;
        public int Stat { get; } = 1;
        public int Value { get; } = 1;
        public string Name { get; } = "종이 조각";
        public string Desc { get; } = "수첩에서 떨어져나온 종이 조각이다. 뭐라고 쓰여져 있는 것 같은데...";
        public int RecoveryAmount { get; } = 1;

        private Player Player { get; } = GameManager.Instance.GameData.Player;

        public void Use(ICharacter? target)
        {
            Console.WriteLine($"찢어져서 잘 보이지 않지만 {"밀번호가 passw"} 라고 써져있는 것 같다.");
        }
    }
}
