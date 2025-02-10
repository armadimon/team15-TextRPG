using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    public interface ISKill
    {
            public string SkillName { get; set; }
            public string Description { get; set; }
            public int BonusDamage { get; set; }
            public int SkillCost { get; set; }
            public string SkillType { get; set; }     
    }

    public class RailGun : ISKill
    {
        public string SkillName { get; set; } = "소형 레일건";
        public string Description { get; set; } = "대상을 원거리에서 공격합니다.";
        public int BonusDamage { get; set; } = 10;
        public int SkillCost { get; set; } = 5;
        public string SkillType { get; set; } = "Human";
    }
}
