using _15TextRPG.Source.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.State
{
    internal interface IHackable : ICharacter
    {
        int HackDefenderLV { get; set; }
        HackState HackState { get; }
        bool HackingProcess(string userCommand);
    }
}
