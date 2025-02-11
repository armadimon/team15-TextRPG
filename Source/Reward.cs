using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    public class Reward
    {
        public int Exp { get; set; }  // 경험치 보상
        public List<IItem> Items { get; set; }  // 아이템 보상

        public Reward(int exp, List<IItem> items)
        {
            Exp = exp;
            Items = items ?? new List<IItem>();
        }

        public override string ToString()
        {
            return $"경험치: {Exp}, 아이템: {string.Join(", ", Items)}";
        }
    }
}
