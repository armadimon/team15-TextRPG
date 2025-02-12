using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Text;

namespace _15TextRPG.Source.State
{
    internal class HackState(Func<string, bool> HackingProcess, int limitTimeSec, string[]? _commandList = null) : IGameState
    {
        string[] commandList = _commandList ?? [];

        List<string> commands = [];
        int commandIndex = -1;

        string command = "";

        public void DisplayMenu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "Hacking in Progress...";
            Console.Clear();

            Console.WriteLine("Hacking in progress...");
            Console.WriteLine("Time Remaining:");

            Console.SetCursorPosition(0, 2);
            Console.WriteLine("?!@##$$%");
            Console.WriteLine($" ?@!#$ sec remaining");
            Console.WriteLine("==========================================");
        }


        public void HandleInput()
        {
            int totalTime = limitTimeSec * 100; // 해킹 제한 시간 (초)
            int barLength = 3000; // 진행 바 길이

            int cursorTop, cursorLeft;

            foreach (char c in "사이버테크 멀웨어 작동 중...")
            {
                Console.Write(c);
                Thread.Sleep(50);
            }
            Console.WriteLine();
            for(int i = 0; i < 5; ++i)
            {
                foreach (char c in "------")
                {
                    Console.Write(c);
                    Thread.Sleep(50);
                }
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new String(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
            }
            Console.WriteLine();
            foreach (char c in "실행 완료. 슈퍼 베이비 시팅 모드가 필요하면 help를 입력하세요.\n\n해킹을 시작하시겠습니까? [y/n]")
            {
                Console.Write(c);
                Thread.Sleep(50);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.Write(">> ");
            command = Console.ReadLine();
            if (command == "y")
            {
            }
            else if(command == "help")
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (char c in "슈퍼 베이비 시팅 모드를 시작합니다. 사람으로 태어나 기계보다 못 한 주인님을 위해 제가 모든 걸 해결할게요.")
                {
                    Console.Write(c);
                    Thread.Sleep(50);
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (char c in "열려 있는 포트 확인, 서비스 버전 탐색, 취약한 웹 애플리케이션 찾기")
                {
                    Console.Write(c);
                    Thread.Sleep(20);
                }
                Console.WriteLine();
                Console.WriteLine();
                foreach (char c in "패스워드 크래킹, SQL 인젝션, 피싱 공격")
                {
                    Console.Write(c);
                    Thread.Sleep(10);
                }
                Console.WriteLine();
                Console.WriteLine();
                foreach (char c in "관리자 권한 획득, 백도어 설치, 루트킷 배포")
                {
                    Console.Write(c);
                    Thread.Sleep(20);
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (char c in "완료했어요. 7살 강아지 정도는 되야 풀 수 있는 수준이니 상심하지 마세요.")
                {
                    Console.Write(c);
                    Thread.Sleep(50);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.ReadLine();
                GameManager.Instance.ChangeState(new ExploreState(GameManager.Instance.GameData.CurrentChapter.CurrentStage.Name));
                return;
            }
            else
            {
                Console.WriteLine();
                foreach (char c in "해킹을 중단합니다.")
                {
                    Console.Write(c);
                    Thread.Sleep(50);
                }
                Console.ReadLine();
                GameManager.Instance.ChangeState(new ExploreState(GameManager.Instance.GameData.CurrentChapter.CurrentStage.Name));
                return;
            }
            command = "";

            for (int timeLeft = totalTime; timeLeft >= 0; timeLeft--)
            {
                cursorTop = Console.CursorTop;
                cursorLeft = Console.CursorLeft;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Hacking in progress...                                ");
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("Time Remaining:                                       ");
                int filled = (int)((timeLeft / (float)totalTime) * barLength);
                string progressBar = "[" + new string('█', filled / 100) + new string(' ', (barLength - filled) / 100) + "                   ";

                Console.SetCursorPosition(0, 2);
                Console.WriteLine(progressBar);
                Console.WriteLine($" {timeLeft}                                                    ");
                Console.WriteLine("==========================================");
                Console.SetCursorPosition(cursorLeft, cursorTop);

                ConsoleKeyInfo key;
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Enter)
                    {
                        if (HackingProcess(command))
                        {
                            Console.WriteLine("\nHacking Success!");
                            GameManager.Instance.ChangeState(new ExploreState(GameManager.Instance.GameData.CurrentChapter.CurrentStage.Name));
                            return;
                        }
                        if(command.Length > 0) commands.Add(command);
                        Console.WriteLine();
                        command = "";
                        commandIndex = -1;
                        Console.Write(">> ");
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (command.Length > 0)
                        {
                            command = command.Substring(0, command.Length - 1);

                            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }
                    }
                    else if (key.Key == ConsoleKey.UpArrow)
                    {
                        if (commands.Count > 0)
                        {
                            if (commands.Count - 1 > commandIndex)
                            {
                                ++commandIndex;
                            }
                            command = commands[commands.Count - 1 - commandIndex];
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(">> ");
                            Console.Write(command);
                        }
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        if (commands.Count > 0)
                        {
                            if (commandIndex > 0)
                            {
                                --commandIndex;
                            }
                            command = commands[commands.Count - 1 - commandIndex];
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(">> ");
                            Console.Write(command);
                        }
                    }
                    else if (key.Key == ConsoleKey.Tab)
                    {
                        if (command.Length > 0)
                        {
                            foreach (string _ in commandList)
                            {
                                if (_.Contains(command) && _ != command)
                                {
                                    Console.SetCursorPosition(0, Console.CursorTop);
                                    Console.Write(">> " + _);
                                    command = _;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        command += key.KeyChar;
                    }
                }

                Thread.Sleep(10);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\r\n\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@###$*!;;;;;*$##@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#=~             ~$#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#=-                 -=#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#:                     :#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#~                       -$@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#~                         ~$@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#~                           ~#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#:                             !@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@*   ..                         -$@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#~   ~~                          !@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=   ,=:                          :#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#;   ;=,                          .=@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$,  -$!                            *@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$   ;$~                            *@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$  -$*                             *@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$  ~#:                             *@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$  ~=.                             *@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$  ~*     ,:-          .:~         *@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$  ~*   .;$#$=,       =$##*-       *@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$  ~*   ;@@@@@#.     $@@@@@*.  ~:  *@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$. ~*  :@@@@@@@:    .@@@@@@@!  ;;  *@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#: ~*  ;@@@@@@@*.   ~@@@@@@@*  ;; .=@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@= ~* .*@@@@@@@-     @@@@@@@=. ;; :#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$-~* ,*@@@@@@@,     @@@@@@@$- ;; !@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=;! .*@@@@@@:      .@@@@@@#~ ;**@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#:  ;@@@@@=.  :*   ,@@@@@*  ;@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#~   ~#@@@-    #@    ,@@@=   ~#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=       .      ~@@               !@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=              #@@,              !@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$.             #@@#              *@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=.           ,#@@@            .!@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$-.         !$!=$          .,!@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=~,       ~            .,!@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#;.                 ,;@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@!.               ~$@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$=#@!.             -$@$$@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@; :#@!,           -=#!-~@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@=**#@@@@@@@@@@@@@@@;  !@@$:~~~~~~~~~:$@*  ~@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@=!,  :$@@@@@@@@@@@@@@;  ~#@=!#*@*$*!@!@@#-  ~@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@=,     -#@@@@@@@@@@@@@;   *@- $,#.~. @.@@$   ~@@@@@@@@@@@@@@@@!!!!*@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@#~       :@@@@@@@@@@@@@;   *@*;#!@;;;;@;@@$   :@@@@@@@@@@@@@@@;    .*@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@$:.        ;@@@@@@@@@@@@;   *@@@@@@@@@@@@@@*  .;@@@@@@@@@@@@@@;      .*@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@$:-           =@@@@@@@@@@@!   :#@@@@@@@@@@@@#,  ,=@@@@@@@@@@@@@*        -@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@$-             -@@@@@@@@@@@$-  .=@!#;#;:@#:@@$   -#@@@@@@@@@@@@#,        .*@@@@@@@@@@@@@@@@\r\n@@@@@@@@#:               ~#@@@@@@@@@@:   :#-$,#. @~ @#~   ~@@@@@@@@@@@@@~          .:=@@@@@@@@@@@@@@\r\n@@@@@@@@=.                ~$@@@@@@@@@=-  .~!@*@,*@**@~   ,=@@@@@@@@@@@@:             ,*@@@@@@@@@@@@@\r\n@@@@@@@@!                  ,;@@@@@@@@@;    -~~~.~~~~-    ;@@@@@@@@@@@=~               ,=@@@@@@@@@@@@\r\n@@@@@@@#:       ;,           :@@@@@@@@#~                ,#@@@@@@@@@@;,                 ;@@@@@@@@@@@@\r\n@@@@@@@@!       *$*.          -;@@@@@@@#-              ,$@@@@@@@@@$:                   ,*@@@@@@@@@@@\r\n@@@@@@@@*       .,-*$*          -!@@@@@@#-            .$@@@@@@@@@;,            -        !@@@@@@@@@@@\r\n@@@@@@@@@*,        .,,!=         .-*@@@@@#-          .$@@@@@@@@;,             ;=        ;@@@@@@@@@@@\r\n@@@@@@@@@@#$;.        ..           .-=@@@@##$$$$$$####@@@@@@#:.           :$$:,.       ~#@@@@@@@@@@@\r\n@@@@@@@@@@@@@####=,                  .:#@@@@@@@@@@@@@@@@@@$-.          ~$;...         ,=@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@##:                  !@@@@@@@@@@@@@@@$,                          -=@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@#:                 =@@@@@@@@@@@$.                      ;##@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@#.               .$@@@@@@@#.                   ,#@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@!               -#@@@@.                  =@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=.              ;@@                .*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=-              ##.           .!@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$:.            .#=.      .:#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$:.            -$!. .-*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#=~.            ;=!#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#$~ ;!-.           ,=#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#=:    .!*~.           -=#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=*~        ~*!~            ~=#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=*!.            !#:~            ~=#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@$******=====*****;.              -~$@@~-           .:*$@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@$;,      .....                   -:$@@@@@#~,            -!*=#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@$~                             .~;#@@@@@@@@@=:,             .:!!!!!!!**$#@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@!    -~.                     ,:*#@@@@@@@@@@@@@=:.                     .-:!!*#@@@@@@@@@@@@@@@\r\n@@@@@@@#;    -~.                   ,;*@@@@@@@@@@@@@@@@@@*:.                        .:=@@@@@@@@@@@@@@\r\n@@@@@@@@!                        ~;=@@@@@@@@@@@@@@@@@@@@@#*:.    -~                  :@@@@@@@@@@@@@@\r\n@@@@@@@@=.                     :!$@@@@@@@@@@@@@@@@@@@@@@@@@#*!~  --:.          .:-   ~#@@@@@@@@@@@@@\r\n@@@@@@@@#*-      ~.          -*#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$!,  -:,          ~,   ~#@@@@@@@@@@@@@\r\n@@@@@@@@@@$*-    *-         *$@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=-  -~-              ~@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@$-   *=!*,    -*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$~  .~;            ,*@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@;   ,;;~.   !$@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#;. .-!          ~=@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@#~   ..   -=@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#*.  -!==;   ,==#@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@#-     ,=$@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=.  ,--,  .$@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@#=;~!=$@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$.      .=@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#*    .$@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@##=##@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n");
            Console.WriteLine("\nHACKING FAILED! SYSTEM SHUTDOWN...");

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ReadLine();
        }
    }
}
