using _15TextRPG.Source.Chapter1;
using _15TextRPG.Source.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.HackExample
{
    internal class YSMChapter : ChapterData
    {
        Player Player { get; }
        public YSMChapter(string name, Player player) : base(name)
        {
            Player = player;
        }

        public new void InitailizeChapter1()
        {
            //StageData stage1 = new StageData("Stage1", 30, 20);
            //StageData stage2 = new StageData("Stage2", 30, 20);
            //AddStage(stage1);

            //stage1.SetTile(15, 10, TileType.CCTV, new Computer(Player));
            //NPC npc1 = new NPC("니콜라스", "메가코프사 경비. 메가코프사의 다양한 '지원'을 받아서 강한 전투력을 얻었지만, 수명은 얼마 남지않았다.", (12, 12));
            //NPC npc2 = new NPC("헥터 ", "메가코프사 경비. 메가코프사의 다양한 '지원'을 받아서 강한 전투력을 얻었지만, 수명은 얼마 남지않았다.", (15, 15));
            //nowPlay = npc1;
            //NPCs.Add(npc1);
            //NPCs.Add(npc2);
            //stage1.SetTile(12, 12, TileType.NPC, npc1);
            //stage1.SetTile(15, 15, TileType.NPC, npc2);
            //stage1.SetTile(15, 0, TileType.CCTV, new CCTV());
            //AddStage(stage2);
            //CurrentStage = stage1;
            //BossMob boss = new BossMob();
            //stage2.SetTile(15, 15, TileType.Boss, boss);
            //AddQuestEvent("FindPass", "패스워드를 찾으세요.");
            //AddQuestEvent("DefeatBoss", "보스를 처치하세요."); 
        }
    }
}
