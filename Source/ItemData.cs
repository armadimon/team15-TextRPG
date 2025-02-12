using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    public enum ItemList
    {
        HpRecovery = 1,
        StrUpPotion

    }
    public static class ItemData
    {
        public static readonly List<IItem> RecoveryItems = new List<IItem>
        {
            new RecoveryItem(),
        };

        public static readonly List<IItem> StateUpgradeItems = new List<IItem>
        {
            new StateUpgradeItem(),
        };


    }
}
