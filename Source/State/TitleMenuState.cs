using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.State
{

    public class TitleMenuState : IGameState
    {
        private static ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Magenta };
        private static int colorIndex = 0; // 색상 변경 인덱스
        private bool isRunning = true; // 스레드 실행 여부
        private int curTop = 0;
        private int curLeft = 0;


        public void DisplayMenu()
        {
            Console.Clear();

            //string imagePath = "D:\\csprac\\15TextRPG\\image\\4.bmp"; // BMP 이미지 파일

            string imagePath = "..\\..\\..\\image\\4.bmp"; // BMP 이미지 파일
            int width = 50; // 출력할 너비

            string ascii = AsciiArtRenderer.ConvertBmpToAscii(imagePath, width);

            int artX = Console.WindowWidth; // 아트 중앙 x -> 나중에 콘솔 크기 정하면 그거 중앙으로
            int artY = Console.WindowHeight; // 아트 중앙 y -> 나중에 콘솔 크기 정하면 그거 중앙으로 하거나 뭐 대충
            Console.OutputEncoding = Encoding.UTF8; // 한글 및 특수문자 깨짐 방지

            Task.Run(() => AnimateAsciiArt(ascii));
        }

        public void HandleInput()
        {
            // 콘솔크기 고정되면 이거 쓸 수 있음
            //Console.SetCursorPosition(0, Console.WindowHeight - 1);
            //Console.Write("\n원하시는 행동을 입력해주세요. >> ");

            string input = Console.ReadLine() ?? "";
            isRunning = false;

            switch (input)
            {
                case "1":
                    NewGame(GameManager.Instance);
                    GameManager.Instance.ChangeState(new MainMenuState()); // 메인메뉴로
                    break;
                case "2":
                    Console.WriteLine("이어하기");
                    break;
                case "3":
                    Console.WriteLine("크레딧");
                    break;
                case "0":
                    GameManager.Instance.QuitGame();
                    break;
                default:
                    isRunning = true;
                    break;
            }
        }

        private void AnimateAsciiArt(string ascii)
        {
            while (isRunning) // isRunning이 true일 때만 실행
            {
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = colors[colorIndex]; // 색상 변경
                AsciiArtRenderer.PrintAsciiArt(0, 0, ascii); // 아스키 아트 출력
                curTop = Console.CursorTop;
                curLeft = Console.CursorLeft;
                Console.ResetColor();
                colorIndex = (colorIndex + 1) % colors.Length;

                Console.WriteLine("\n");
                int menuWidth = 100;
                int menuX = (Console.WindowWidth - menuWidth) / 2;
                int menuY = curTop + 2; // 아트 아래 출력

                AsciiArtRenderer.DrawMenu(menuX, menuY, 100, 7);
                Thread.Sleep(2500); // 2.5초마다 색상 변경
                curTop = Console.CursorTop;
                curLeft = Console.CursorLeft;

            }
        }

        private void NewGame(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("[시작]");
            Console.WriteLine("\n어서와, 그래. 당신 이름이 뭐라고 했지?");

            Console.WriteLine("\n당신의 이름을 입력해주세요");
            Console.Write(">> ");
            Console.ForegroundColor = ConsoleColor.Green;
            gameManager.GameData.Player.Name = Console.ReadLine();
            Console.Write($"\n{gameManager.GameData.Player.Name}");
            Console.ResetColor();
            Console.WriteLine($"...? 그래. 좋은 이름이군");


            Console.Write($"그럼 ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{gameManager.GameData.Player.Name}");
            Console.ResetColor();
            Console.WriteLine($". 여기는 무슨 볼일로 온 거지?\n");



            while (true)
            {
                Console.Write("1. 원래 방랑자로 살아왔으나 최근 무리를 떠나 홀로 도시 생활을 시작하러 왔다.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("(노마드)");
                Console.ResetColor();


                Console.Write("2. 본래 이쪽 출신이었으나 잠시 동부에 머물렀다, 다시 돌아온 참이었다.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("(부랑아)");
                Console.ResetColor();


                Console.Write("3. 기업의 임무를 처리하러 왔다.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("(기업)");
                Console.ResetColor();



                Console.WriteLine("\n당신의 직업을 입력해주세요");
                Console.Write(">> ");
                Console.ForegroundColor = ConsoleColor.Green;
                if (int.TryParse(Console.ReadLine(), out int input) && Enum.IsDefined(typeof(Job), input))
                {           
                    Job choice = (Job)input;
                    gameManager.GameData.Player.Job = choice;
                    gameManager.GameData.Player.StartStat(choice);
                    Console.ResetColor();

                    break;
                }
                else
                {
                    Console.ResetColor();
                    Console.WriteLine("뭐라고? 다시 한번 말해주겠나.");
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\n{GameData.JobDescriptions[gameManager.GameData.Player.Job]}");
            Console.ResetColor();
            Console.WriteLine($"? 그렇군. 각자의 사정이 있는거겠지.");

            Console.WriteLine("밖은 위험하니 몸 조심해. 또 보자고 친구.");

            Console.WriteLine("\n아무키나 입력하여 밖으로 나가기");
            Console.ReadLine();


        }
    }
}
