using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source;
using _15TextRPG.Source.State;

namespace _15TextRPG.Source.Chapter1
{

    public class ChangeStage : IInteractableObject
    {
        private StageData nextStage;
        private bool isOpen = false;

        public ChangeStage(StageData nextStage, bool isOpen)
        {
            this.isOpen = isOpen;
            this.nextStage = nextStage;
        }

        public void Interact()
        {
            if (isOpen == false)
            {
                Console.WriteLine("보안이 걸린 문이다.\n");
                Console.WriteLine("1. 억지로 연다");
                Console.WriteLine("2. 패스워드를 입력한다.");
                string input = Console.ReadLine() ?? "";
                switch (input)
                {
                    case "1":
                        if (GameManager.Instance.GameData.Player.Str >= 3)
                        {
                            Console.WriteLine("문을 열었습니다.");
                            GameManager.Instance.GameData.CurrentChapter.CurrentStage = nextStage;
                        }
                        else
                        {
                            Console.WriteLine("문을 열기에는 힘이 부족합니다. 필요 STR : 3");
                        }
                        Console.ReadLine();
                        break;
                    case "2":
                        if (GameManager.Instance.GameData.CurrentChapter.QuestItems.Find(q => q.Name == "FindPass").IsGet == true)
                        {
                            Console.WriteLine("문이 열렸습니다.");
                            Console.ReadLine();
                            GameManager.Instance.GameData.CurrentChapter.CurrentStage = nextStage;
                        }
                        else
                        {
                            Console.WriteLine("올바른 패스워드가 필요합니다.");
                            Console.ReadLine();
                        }
                        break;
                }
            }
            else
            {
                GameManager.Instance.GameData.CurrentChapter.CurrentStage = nextStage;
            }
        }
    }

    public class Exit : IInteractableObject
    {


        public Exit()
        {
        }

        public void Interact()
        {
            Console.WriteLine("1. 탈출한다");
            Console.WriteLine("2. 아직 할일이 남아있다.");
            string input = Console.ReadLine() ?? "";

            switch (input)
            {
                case "1":
                    GameManager.Instance.ChangeState(new MainMenuState());
                    break;
                case "2":
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

            public void Interact()
            {
                if (IsHacked == true)
                {
                    Console.WriteLine("1. 살펴보기");
                    Console.WriteLine("2. 그만두기");
                    string input = Console.ReadLine() ?? "";
                    ExploreState temp = new ExploreState(GameManager.Instance.GameData.CurrentChapter.CurrentStage.Name);

                    switch (input)
                    {
                        case "1":
                            temp.CCTVMode();
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

                    switch (input)
                    {
                        case "1":
                            GameManager.Instance.ChangeState(HackState);
                            break;
                        case "2":
                            break;
                    }
                }
            }

            public bool HackingProcess(string command)
            {
                Console.WriteLine($"Executing command: {command}");
                if (command == "help")
                {
                    foreach (char c in "[네트워크 스캔 중...]\n")
                    {
                        Console.Write(c);
                        Thread.Sleep(50);
                    }
                    Console.WriteLine("CCTV - 01(보안 수준: 낮음)\n");
                    foreach (char c in "[보조 AI: \"이 CCTV는 보안 수준이 낮습니다. 'hack CCTV-01'로 패스워드 크랙을 시도하세요.\"]")
                    {
                        Console.Write(c);
                        Thread.Sleep(50);
                    }
                }
                if (command == "hack CCTV-01  ")
                {
                    foreach (char c in "[패스워드 크랙 시도 중...] ")
                    {
                        Console.Write(c);
                        Thread.Sleep(50);
                    }
                    Console.WriteLine($"접속 성공!\n");
                    foreach (char c in "[실시간 카메라 피드 로드 중...]\n")
                    {
                        Console.Write(c);
                        Thread.Sleep(50);
                    }
                    IsHacked = true;
                    Thread.Sleep(500);
                    return (true);
                }
                return (false);
            }
        }


        public class EnemyTrigger : IInteractableObject
        {
            public void Interact()
            {
                GameManager.Instance.ChangeState(new CombatState());
            }
        }

        public class PasswordForStage1 : IInteractableObject
        {
            private string password;

            public PasswordForStage1(string password)
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
                    GameManager.Instance.GameData.CurrentChapter.CompleteQuest("FindPass");
                    Console.ReadLine();
                }
            }
        }
        public class Database : IInteractableObject, IHackable
        {

        public bool IsHacked { get; set; }
        public string Name { get; set; }
        internal HackState HackState { get; private set; }
        HackState IHackable.HackState => throw new NotImplementedException();
        public Database()
        {
            IsHacked = false;
            HackState = new HackState(HackingProcess, 20);
        }

        public void Interact()
        {
            if (IsHacked == true)
            {
                Console.WriteLine($"더이상 얻을 수 있는 정보는 없다");
                string input = Console.ReadLine() ?? "";
            }
            else
            {
                Console.WriteLine($"1. 해킹");
                Console.WriteLine($"0. 나가기");
                string input = Console.ReadLine() ?? "";
                switch (input)
                {
                    case "1":
                        GameManager.Instance.ChangeState(HackState);
                        break;
                    case "0":
                        break;
                }
            }
        }
        public bool HackingProcess(string command)
        {
            Console.WriteLine($"Executing command: {command}");
            if (command == "search")
            {
                foreach (char c in "데이터 베이스 확인중...")
                {
                    Console.Write(c);
                    Thread.Sleep(50);
                }
                Console.WriteLine($"아래와 같은 파일을 확인하였습니다.");
                Console.WriteLine($"[1] 업무 이메일");
                Console.WriteLine($"[2] 2077/12/10");
            }
            if (command == "1")
            {
                Console.WriteLine($"내용 1");
            }
            if (command == "2")
            {
                Console.WriteLine($"내용 2");
            }
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
                QuestManager.Instance.UpdateQuest("기밀 문서를 입수하라", 0);
                IsHacked = true;
                return (true);
            }
            return (false);
        }
    }

        public class BossMob : IInteractableObject, IHackable
        {

            public bool IsHacked { get; set; }
            public string Name { get; set; }
            internal HackState HackState { get; private set; }
            HackState IHackable.HackState => throw new NotImplementedException();
            public BossMob(string name)
            {
                Name = name;
                IsHacked = false;
                HackState = new HackState(HackingProcess, 20);
            }

            public void Interact()
            {
                if (IsHacked == true)
                {
                    Console.WriteLine($"1. 기밀문서 획득");
                    string input = Console.ReadLine() ?? "";
                    switch (input)
                    {
                        case "1":
                            Console.WriteLine($"미션 완료!!");
                            Console.ReadLine();
                            GameManager.Instance.ChangeState(new MainMenuState());
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"1. 전투");
                    Console.WriteLine($"2. 해킹");
                    string input = Console.ReadLine() ?? "";
                    switch (input)
                    {
                        case "1":
                            Console.WriteLine($"NPC: 죽인다!!");
                            Console.ReadLine();
                            GameManager.Instance.ChangeState(new BattleMenuState());
                            
                            break;
                        case "2":
                            GameManager.Instance.ChangeState(HackState);
                            if (IsHacked == true)
                            {
                                GameManager.Instance.GameData.CurrentChapter.CompleteQuest("DefeatBoss");
                            }
                            break;
                    }
                }
            }
            public bool HackingProcess(string command)
            {
                Console.WriteLine($"Executing command: {command}");
                if (command == "search")
                {
                    foreach (char c in "전뇌 데이터 베이스 확인중...")
                    {
                        Console.Write(c);
                        Thread.Sleep(50);
                    }
                    Console.WriteLine($"아래와 같은 파일을 확인하였습니다.");
                    Console.WriteLine($"[1] 업무 이메일");
                    Console.WriteLine($"[2] 2077/12/10");
                }
                if (command == "1")
                {
                    Console.WriteLine($"내용 1");
                }
                if (command == "2")
                {
                    Console.WriteLine($"내용 2");
                }
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
                    QuestManager.Instance.UpdateQuest("보스를 처치하라", 0);
                    IsHacked = true;
                    return (true);
                }
                return (false);
            }
        }
    }


