using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source.State;

namespace _15TextRPG.Source
{
    public interface IInteractableObject
    {
        void Interact();
    }

    public enum TileType
    {
        Empty,
        Wall,
        ChangeStage,
        Battle,
        NPC,
        Password,
        Boss,
        CCTV,
        ETC
    }

    public class NPC : IInteractableObject
    {
        public string Name { get; set; }
        public string RevealedName { get; set; }
        public string Desc { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public int Dir { get; set; }
        public int DefensePoint { get; set; }
        public int Health { get; set; }
        public int AttackDamage { get; set; }
        public bool IsHacked { get; set; }
        public string StageName { get; set; }

        public NPC(string name, string stageName, string desc, (int, int) npcPos)
        {
            posX = npcPos.Item1;
            posY = npcPos.Item2;
            Dir = 1;
            Name = name;
            StageName = stageName;
            Desc = desc;
            RevealedName = new string('*', name.Length);
            Health = 100;
            AttackDamage = 5;
            DefensePoint = 5;
            IsHacked = false;
        }

        public void Interact()
        {
            Console.WriteLine("1. 전투");
            string input = Console.ReadLine() ?? "";

            switch (input)
            {
                case "1":
                    GameManager.Instance.ChangeState(new BattleMenuState());
                    break;
            }
            Console.ReadLine();
        }
    }

    public class Tile
    {
        public TileType Type { get; private set; }
        public IInteractableObject? Object { get; private set; }

        public Tile(TileType type, IInteractableObject? obj = null)
        {
            Type = type;
            Object = obj;
        }
    }

    public class EnemyTrigger : IInteractableObject
    {
        public int posX { get; set; }
        public int posY { get; set; }

        public EnemyTrigger((int, int) enemyPos)
        {
            posX = enemyPos.Item1;
            posY = enemyPos.Item2;
        }
        public void Interact()
        {
            Console.ReadLine();
            GameManager.Instance.ChangeState(new BattleMenuState(this));
        }

        public void Defeat()
        {
            GameManager.Instance.GameData.CurrentChapter.CurrentStage.SetTile(posX, posY, TileType.Empty);
        }
    }

    public class Password : IInteractableObject
    {
        private string password;

        public Password(string password)
        {
            this.password = password;
        }

        public void Interact()
        {
            Console.WriteLine($"패스워드를 입력하세요.");
            string pass = Console.ReadLine() ?? "";
            if (pass == password)
            {
                Console.WriteLine("권한을 획득하였습니다!");
                Console.ReadLine();
            }
        }
    }
}
