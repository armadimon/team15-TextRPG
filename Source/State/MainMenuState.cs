using System;

namespace _15TextRPG.Source.State
{
    public class MainMenuState : IGameState
    {
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("\n[캠프 입구]");
            string imagePath = "..\\..\\..\\image\\map3.bmp"; // BMP 이미지 파일
            int width = 70; // 출력할 너비

            string ascii = AsciiArtRenderer.ConvertBmpToAscii(imagePath, width);

            AsciiArtRenderer.PrintAsciiArt(0, ascii); // 아스키 아트 출력

            Console.WriteLine("\n1. 상태 보기");
            Console.WriteLine("\n2. 의뢰 수주");
            Console.WriteLine("\n3. 챕터 선택");
            Console.WriteLine("\n4. hn전투");
            Console.WriteLine("\n5. JW전투");
            Console.WriteLine("\n6. 회복하기");

            Console.WriteLine("\n0. 종료");
        }

        public void HandleInput()
        {
            GameManager.Instance.GameData.CurrentChapter = null;
            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
            string input = Console.ReadLine() ?? "";

            switch (input)
            {
                case "1":
                    GameManager.Instance.ChangeState(new StatusMenuState());
                    break;
                case "2":
                    GameManager.Instance.ChangeState(new ContractState());
                    break;
                case "3":
                    GameManager.Instance.ChangeState(new ChapterState());
                    break;
                case "4":
                    //GameManager.Instance.ChangeState(new JHNCombatState());
                    break;
                case "5":
                    GameManager.Instance.ChangeState(new BattleMenuState());
                    break;
                case "6":
                    GameManager.Instance.ChangeState(new InventoryState());
                    break;
                case "0":
                    GameManager.Instance.QuitGame();
                    break;
            }
        }
    }
}