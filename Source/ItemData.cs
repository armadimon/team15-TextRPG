using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    public static class ItemData
    {
        public static readonly List<IItem> RecoveryItems = new List<IItem>
        {
            new RecoveryItem(1, 1, 1, "Hp포션", "체력을 회복시켜주는 포션이다.", 100),
        };
   
    }
}
