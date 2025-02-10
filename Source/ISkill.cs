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
            public int SkillDamage { get; set; }
            public int SkillCost { get; set; }
            public string SkillType { get; set; }     
    }

    public class RailGun : ISKill
    {
        public string SkillName { get; set; } = "소형 레일건";
        public string Description { get; set; } = $"대상에게 20의 데미지를 입힙니다";
        public int SkillDamage { get; set; } = 20;
        public int SkillCost { get; set; } = 5;
        public string SkillType { get; set; } = "Physics";
    }
}
