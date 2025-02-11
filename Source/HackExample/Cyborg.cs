using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.State
{
    internal class Cyborg : IHackable
    {
        public HackState HackState { get; }

        public Cyborg() 
        { 
            HackState = new HackState(HackingProcess, 10);
        }

        public bool HackingProcess(string command)
        {
            if (command == "ITEM ICE_NO_33")
            {
                string msg = "아이스 브레이커를 사용하셨습니다.";
                foreach (char c in msg)
                {
                    Console.Write(c);
                    Thread.Sleep(50);
                }
            }
            if (command == "search")
            {
                Console.WriteLine("[SYSTEM_WARE][01EEA32] 방화벽 등급 12");
                Console.WriteLine("[COMPUTER][유승민] 방화벽 등급 2");
            }
            if (command == "HACK 01EEA32")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                string msg = "고등급 방화벽에 침투합니다. 20.08ms가 지나면 감지됩니다.";
                foreach (char c in msg)
                {
                    Console.Write(c);
                    Thread.Sleep(50);
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Green;
            }
            return false;
        }
    }
}
