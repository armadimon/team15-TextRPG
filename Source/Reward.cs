using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _15TextRPG.Source
{
    public class Reward
    {
        public List<IItem> Items { get; set; }  // 아이템 보상
        public Reward(List<IItem> items)
        {
            Items = items ?? new List<IItem>();
        }
        public override string ToString()
        {
            return $"아이템: {string.Join(", ", Items)}";
        }
    }
}