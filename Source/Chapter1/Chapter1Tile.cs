using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                    if (gameManager.GameData.CurrentChapter.QuestItems.Find(q => q.Name == "FindPass").IsGet == true)
                    {
                        Console.WriteLine("문이 열렸습니다.");
                        Console.ReadLine();
                        gameManager.GameData.CurrentChapter.CurrentStage = gameManager.GameData.CurrentChapter.Stages[1];
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

    public class CCTV : IInteractableObject, IHackable
    {
        public bool IsHacked { get; set; }
        internal HackState HackState { get; private set; }

        HackState IHackable.HackState => throw new NotImplementedException();

        public CCTV()
        {
            IsHacked = false;
            HackState = new HackState(HackingProcess, 20);
        }

        public void Interact(GameManager gameManager)
        {
            if (IsHacked == true)
            {
                Console.WriteLine("1. 살펴보기");
                Console.WriteLine("2. 그만두기");
                string input = Console.ReadLine() ?? "";
                ExploreState temp = new ExploreState(gameManager.GameData.CurrentChapter.CurrentStage.Name);

                switch (input)
                {
                    case "1":
                        temp.enterCCTVMode(gameManager);
                        break;
                    case "2":
                        break;
                }
            }
            else
            {
                Console.WriteLine("1. 접근 권한 얻기");
                Console.WriteLine("2. 그만두기");
                string input = Console.ReadLine() ?? "";
                ExploreState temp = new ExploreState(gameManager.GameData.CurrentChapter.CurrentStage.Name);

                switch (input)
                {
                    case "1":
                        gameManager.ChangeState(HackState);
                        break;
                    case "2":
                        break;
                }
            }
        }

        public bool HackingProcess(string command)
        {
            Console.WriteLine($"Executing command: {command}");
            if (command == "ITEM ICE")
            {
                foreach (char c in "아이스 브레이커를 사용하셨습니다.")
                {
                    Console.Write(c);
                    Thread.Sleep(50);
                }
                foreach (char c in "시스템 방화벽 해제...")
                {
                    Console.Write(c);
                    Thread.Sleep(50);
                }
                IsHacked = true;
                return (true);
            }
            return (false);
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
