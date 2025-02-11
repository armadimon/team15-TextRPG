using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.State
{
    internal class Computer : IHackable
    {
        public HackState HackState { get; }

        Computer()
        {
            HackState = new HackState(HackingProcess, 10);
        }

        public bool HackingProcess(string userCommand)
        {
            throw new NotImplementedException();
        }
    }
}
