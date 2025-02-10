using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using _15TextRPG.Source.Chapter1;
using System.Threading.Tasks;

namespace _15TextRPG.Source.State
{
    internal class ContractState : IGameState
    {

        public void DisplayMenu(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("\n1. [챕터 : 1] X의 의뢰");
            Console.WriteLine("0. 나가기");
        }

        public void HandleInput(GameManager gameManager)
        {
            Console.Write("\n원하시는 챕터를 입력해주세요. >> ");
            string input = Console.ReadLine() ?? "";

            switch (input)
            {
                case "1":
                    CheckContrat1(gameManager);
                    break;
                case "0":
                    gameManager.ChangeState(new MainMenuState());
                    break;
            }
        }

        public void CheckContrat1(GameManager gameManager)
        {
            List<QuestItem> qItems = gameManager.GameData.Chapters[0].QuestItems;
            for (int i = 0; i < qItems.Count; i++)
            {
                if (qItems[i].IsGet == true)
                {
                    Console.WriteLine($"[{qItems[i].Description}] 완료한 의뢰입니다.");
                }
                else
                {
                    Console.WriteLine($"[{qItems[i].Description}] 미완료한 의뢰입니다.");
                }
            }
            Console.ReadLine();
        }
    }
}

