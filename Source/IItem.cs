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
        public string Name { get; } = "HP ����";
        public string Desc { get; } = "HP�� 50��ŭ ȸ�������ִ� �����̴�.";
        public int RecoveryAmount { get; } = 50;
      
        private Player Player { get; } = GameManager.Instance.GameData.Player;


        public void Use(ICharacter? target)
        {
            if (Player.Health == Player.MaxHP)
            {
                Console.WriteLine("�÷��̾�� �̹� �ִ� ü���Դϴ�.");
                return;
            }
            Player.Health = Math.Min(Player.Health + RecoveryAmount, Player.MaxHP);
            Console.WriteLine($"{RecoveryAmount}��ŭ �÷��̾� ȸ���� -> {Player.Health}");
        }
    }

    class StateUpgradeItem : IItem
    {
        public int Tag { get; } = (int)ItemList.StrUpPotion;
        public int Stat { get; } = 1;
        public int Value { get; } = 1;
        public string Name { get; } = "���̳��� ����";
        public string Desc { get; } = "STR ��ġ�� 1���� �����ִ� ����";
        public int RecoveryAmount { get; } = 1;

        private Player Player { get; } = GameManager.Instance.GameData.Player;

        public void Use(ICharacter? target)
        {
            
            Console.WriteLine($"{RecoveryAmount}��ŭ �÷��̾��� STR�� ���� -> {Player.Str}");
        }
    }
    
    class MiliwareIceBreaker : IItem
    {
        public int Tag { get; } = (int)ItemList.StrUpPotion;
        public int Stat { get; } = 1;
        public int Value { get; } = 1;
        public string Name { get; } = "�и��극��Ŀ";
        public string Desc { get; } = "��ŷ�� ���� ����� 5�ܰ� ����ϴ�.";
        
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
        public string Name { get; } = "���� ����";
        public string Desc { get; } = "��ø���� ���������� ���� �����̴�. ����� ������ �ִ� �� ������...";
        public int RecoveryAmount { get; } = 1;

        private Player Player { get; } = GameManager.Instance.GameData.Player;

        public void Use(ICharacter? target)
        {
            Console.WriteLine($"�������� �� ������ ������ {"�й�ȣ�� passw"} ��� �����ִ� �� ����.");
        }
    }
}
