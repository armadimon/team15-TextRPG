using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _15TextRPG.Source
{
    public class Reward
    {
        public List<IItem> Items { get; private set; }  // 아이템 보상
        public int rewardExp { get; private set; } // 경험치 보상
        public int rewardGold { get; private set; } // 골드 보상

        public Reward(List<IItem> items, int rewardExp, int rewardGold)
        {
            Items = items ?? new List<IItem>();
            this.rewardExp = rewardExp;
            this.rewardGold = rewardGold;
        }

        public override string ToString()
        {
            return $"아이템: {string.Join(", ", Items)}";
        }
    }
}