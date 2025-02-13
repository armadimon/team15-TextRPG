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

        public int HackDefenderLV {get; set; }  

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
                if (command == "hack CCTV-01")
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

        public int HackDefenderLV { get; set; } = 5;

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
        public bool HackingProcess(string userCommand)
        {
            string[] command = userCommand.Split(' ');
            if (command.Length == 1)
            {
                switch (command[0])
                {
                    case "search":
                        if (HackState.directory == "base") Console.WriteLine("Email  Diary  Miliware.exe .아무도_모르는_숨겨진_파일$&^^*                    ");
                        else if (HackState.directory == "Email") Console.WriteLine("제정신이야?  안녕하세요,밀리테크입니다.  Home                  ");
                        else if (HackState.directory == "Diary") Console.WriteLine("2025.02.13  Home                                    ");
                        else Console.WriteLine("Home                  ");
                        break;
                    case "item":
                        Console.Write(GameManager.Instance.GameData.Player.Inventory);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (command[0] == "search")
                {
                    if (HackState.directory == "base")
                    {
                        switch (command[1])
                        {
                            case "Home": HackState.directory = "base"; break;
                            case "Email": HackState.directory = "Email"; break;
                            case "Diary": HackState.directory = "Diary"; break;
                            case "Miliware.exe":
                                if (GameManager.Instance.GameData.Player.Intelligence > 0 && command.Length > 2)
                                {
                                    if (command[2] != null && command[2] == "012345")
                                    Console.WriteLine("충분히 해독할 수 있는 프로그램이다. (+Inventory)                ");
                                    GameManager.Instance.GameData.Player.Inventory.Add(new MiliwareIceBreaker());
                                }
                                else
                                {
                                    Console.WriteLine("잘못된 패스워드.                          ");
                                }
                                break;
                            case ".아무도_모르는_숨겨진_파일$&^^*":
                                if (this.HackDefenderLV != 0)
                                {
                                    Console.WriteLine("해독할려면 외부 장비가 필요하다.                                         ");
                                }
                                else
                                {
                                    Console.WriteLine("파일 접근 정보 패킷 네트워크 송출... 보스의 추레한 모습이 인터넷에 배포되었습니다.     ");
                                    QuestManager.Instance.UpdateQuest("기밀 문서를 입수하라", 0);
                                    IsHacked = true;
                                    return (true);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else if (HackState.directory == "Diary")
                    {
                        switch (command[1])
                        {
                            case "Home": HackState.directory = "base"; break;
                            case "2025.02.13": Console.WriteLine("제출기간이 코앞이라고! 비밀번호는 대충 012345로 해놔!                      "); break;
                            default: break;
                        }
                    }
                    else if (HackState.directory == "Email")
                    {
                        switch (command[1])
                        {
                            case "Home": HackState.directory = "base"; break;
                            case "제정신이야?": Console.WriteLine("야 너 제정신이야? 대체 어떤 놈이 밀리테크 소프트웨어를 백업해놔? 당장 지워.              "); break;
                            case "안녕하세요,밀리테크입니다.": Console.WriteLine("채용담당자입니다.\r\n\r\n귀한 시간을 할애하여 밀리테크에 지원해 주셔서 진심으로 감사드립니다.\r\n\r\n\r\n\r\n귀화와 함께하는 방향에 대해 심사숙고하였으나,\r\n\r\n아쉽게도 이번 [밀리테크] 테크니컬 프로그래머 채용에서는 좋은 소식을 전달드리지 못하게 되었습니다.\r\n\r\n\r\n\r\n채용 전형에 성심성의껏 응해 주심에 다시 한번 깊은 감사를 전하며,\r\n\r\n제출해 주신 이력서는 관련 법령이 허용하는 범위 내에서 밀리테크의 인재풀에 저장하여\r\n\r\n향후 유관한 포지션 오픈 시에 다시금 소중히 검토하여 연락드릴 수 있는 점 안내해 드립니다.\r\n\r\n\r\n\r\n비록 이번 전형에서는 인연이 닿지 못했지만\r\n\r\n귀하께서 보여주신 역량과 열정이 어디에서든 결실을 볼 수 있기를 기원하며,\r\n\r\n추후 더 좋은 기회로 다시 만나 뵐 수 있기를 고대하겠습니다.\r\n\r\n\r\n\r\n\r\n\r\n감사합니다."); break;
                            default: break;
                        }
                    }
                }
                else if (command[0] == "item")
                {
                    if (GameManager.Instance.GameData.Player.Inventory.Find(command[1]) != null)
                    {
                        Console.Write($"아이스 브레이커를 사용했습니다.");
                        GameManager.Instance.GameData.Player.Inventory.Use(command[1], this);
                        GameManager.Instance.GameData.Player.Inventory.Subtract(command[1]);
                        Console.WriteLine($" 보안등급 => {HackDefenderLV}");
                    }
                }
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
        public int HackDefenderLV { get; set; } = 5;
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


