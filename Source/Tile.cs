﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source.State;

namespace _15TextRPG.Source
{
    public interface IInteractableObject
    {
        void Interact(GameManager gameManager);
    }

    public enum TileType
    {
        Empty, Wall, ChangeStage, Battle, NPC, Password, Boss
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

    public class ChangeStage : IInteractableObject
    {
        private string nextStage;

        public ChangeStage(string nextStage)
        {
            this.nextStage = nextStage;
        }

        public void Interact(GameManager gameManager)
        {
            Console.WriteLine($"당신은 {nextStage}로 이동했다.");
            gameManager.GameData.ChangeStage(nextStage);
        }
    }

    public class EnemyTrigger : IInteractableObject
    {
        public void Interact(GameManager gameManager)
        {
            Console.WriteLine("적이 나타났다! 전투 시작!");
            gameManager.ChangeState(new BattleMenuState());


        }
    }

    public class NPC : IInteractableObject
    {
        private string message;

        public NPC(string msg)
        {
            message = msg;
        }

        public void Interact(GameManager gameManager)
        {
            Console.WriteLine($"NPC: \"{message}\"");
            Console.ReadLine();
        }
    }

    public class Password : IInteractableObject
    {
        private string password;

        public Password(string password)
        {
            this.password = password;
        }

        public void Interact(GameManager gameManager)
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
