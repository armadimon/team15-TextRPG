using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    public interface IInventoryOwner
    {
        Inventory Inventory { get; }
    }
}
