using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _15TextRPG.Source.State
{
    internal class Computer : IHackable, IInteractableObject
    {
        public HackState HackState { get; }
        public int HackGage { get; set; } = 10;
        public int HackDefenderLV { get; set; } = 3;

        Example ex = new();

        public Computer()
        {
            HackState = new HackState(HackingProcess, 60);
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
                                if (GameManager.Instance.GameData.Player.Intelligence > 0 && command[2] == "012345")
                                {
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
                                    Console.WriteLine("파일 접근 정보 패킷 네트워크 송출... 주인공의 추레한 모습이 인터넷에 배포되었습니다.     ");
                                }
                                break;
                            default :
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

            return false;
        }

        public void Interact()
        {
            GameManager.Instance.ChangeState(HackState);
        }
    }
}
