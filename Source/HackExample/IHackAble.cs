using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.State
{
    internal interface IHackable
    {
        HackState HackState { get; }
        void HackingProcess(string userCommand);
    }
}
