using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using _15TextRPG.Source.Chapter1;
using System.Threading.Tasks;
using System.Collections;

namespace _15TextRPG.Source.State
{
    internal class ContractState : IGameState
    {

        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("\n1. [챕터 : 1] X의 의뢰");
            Console.WriteLine("\n2. 자유 의뢰");
            Console.WriteLine("0. 나가기");
        }

        public void HandleInput()
        {
            Console.Write("\n원하시는 항목을 입력해주세요. >> ");
            string input = Console.ReadLine() ?? "";

            switch (input)
            {
                case "1":
                    GameManager.Instance.ChangeState(new CheckContractState(ChapterID.Chapter1));
                    break;
                case "2":
                    GameManager.Instance.ChangeState(new BattleMenuState());
                    break;
                case "0":
                    GameManager.Instance.ChangeState(new MainMenuState());
                    break;
            }
        }
    }
}

