using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.State
{
    internal class ChapterState : IGameState
    {

        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("\n1. 챕터 1 선택");
            Console.WriteLine("0. 나가기");
        }

        public void HandleInput()
        {
            Console.Write("\n원하시는 챕터를 입력해주세요. >> ");
            string input = Console.ReadLine() ?? "";

            switch (input)
            {
                case "1":
                    GameManager.Instance.GameData.CurrentChapter = GameManager.Instance.GameData.Chapters[0];
                    Console.WriteLine("[임무 브리핑]\n\n");
                    Console.WriteLine("당신은 메가코프내의 고위 인물에게서 기밀 정보를 빼와야 해.");
                    Console.WriteLine("죽여서 그놈의 전뇌를 뽑아오던, 방화벽을 뚫고 정보만 빼내오던");
                    Console.WriteLine("원하는 데이터만 받으면 상관없어. 깔끔하게 처리하면 보수를 더 주지.");
                    Console.WriteLine("죽지 않게 잘해보라구. 뒈져도 우리랑은 상관없으니깐 본인 목숨은 본인이 잘 간수해.");
                    Console.WriteLine("\n\n[메인 퀘스트 : 메가코프의 기밀 정보를 획득하라]");
                    Console.WriteLine("[사이드 퀘스트 : 어딘가에 있는 메가코프의 시험용 무기를 획득하라]");
                    Console.ReadLine();
                    GameManager.Instance.ChangeState(new ExploreState(GameManager.Instance.GameData.CurrentChapter.CurrentStage.Name));
                    break;
                case "0":
                    GameManager.Instance.ChangeState(new MainMenuState());
                    break;
            }
        }
    }
}
