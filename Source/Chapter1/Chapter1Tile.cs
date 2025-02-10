using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source.State;

namespace _15TextRPG.Source.Chapter1
{

    public class ChangeStage2 : IInteractableObject
    {
        private string nextStage;
        private string currentStage;
        private bool isOpen = false;

        public ChangeStage2(string nextStage, bool isOpen)
        {
            this.isOpen = isOpen;
            currentStage = nextStage;
            this.nextStage = nextStage;
        }

        public void Interact(GameManager gameManager)
        {
            Console.WriteLine("보안이 걸린 문이다.");
            Console.WriteLine("1. 억지로 연다");
            Console.WriteLine("2. 패스워드를 입력한다.");
            string input = Console.ReadLine() ?? "";
            switch (input)
            {
                case "1":
                    if (gameManager.GameData.Player.AttackDamage > 5)
                    {
                        Console.WriteLine("문을 열었습니다.");
                        gameManager.GameData.CurrentChapter.CurrentStage = gameManager.GameData.CurrentChapter.Stages[1];
                    }
                    else
                    {
                        Console.WriteLine("문을 열기에는 힘이 부족합니다.");
                    }
                    Console.ReadLine();
                    break;
                case "2":
                    if (isOpen)
                    {
                        //ChangeStage(gameManager);
                    }
                    else
                    {
                        Console.WriteLine("올바른 패스워드가 필요합니다.");
                        Console.ReadLine();
                    }
                    break;
            }
        }
    }

    public class EnemyTrigger : IInteractableObject
    {
        public void Interact(GameManager gameManager)
        {
            Console.WriteLine("적이 나타났다! 전투 시작!");
            gameManager.ChangeState(new CombatState());
        }
    }

    public class NPC : IInteractableObject
    {
        private string message;
        public int posX { get; set; }
        public int posY { get; set; }
        public int Dir { get; set; }
        public NPC(string msg, (int, int) npcPos)
        {
            posX = npcPos.Item1;
            posY = npcPos.Item2;
            Dir = 1;
            message = msg;
        }

        public void Interact(GameManager gameManager)
        {
            Console.WriteLine("1. 대화하기");
            Console.WriteLine("2. 해킹");
            string input = Console.ReadLine() ?? "";

            switch (input)
            {
                case "1":
                    Console.WriteLine($"NPC: \"{message}\"");
                    break;
                case "2":
                    gameManager.ChangeState(new CombatState());
                    gameManager.GameData.CurrentChapter.nowPlay = this;
                    break;
            }
            Console.ReadLine();
        }
    }

    public class PasswordForStage1 : IInteractableObject
    {
        private string password;

        public PasswordForStage1(string password)
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
                gameManager.GameData.CurrentChapter.CompleteQuest("FindPass");
                Console.ReadLine();
            }
        }
    }

    public class BossMob : IInteractableObject
    {

        public BossMob()
        {
        }

        public void Interact(GameManager gameManager)
        {
            Console.WriteLine($"1. 전투");
            Console.WriteLine($"2. 해킹");
            string input = Console.ReadLine() ?? "";
            switch (input)
            {
                case "1":
                    Console.WriteLine($"NPC: 죽인다!!");
                    Console.ReadLine();
                    gameManager.ChangeState(new CombatState());
                    break;
                case "2":
                    Console.WriteLine($"해킹완료");
                    Console.ReadLine();
                    gameManager.GameData.CurrentChapter.CompleteQuest("DefeatBoss");
                    gameManager.ChangeState(new MainMenuState());
                    break;
            }
        }
    }
}
