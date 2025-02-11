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
            StageData stage1 = new StageData("Stage1", 30, 20);
            StageData stage2 = new StageData("Stage2", 30, 20);
            AddStage(stage1);
            Computer computer = new(Player);
            NPC npc1 = new NPC("무슨일이지?", (12, 12));
            NPC npc2 = new NPC("비밀번호는 내가 들고있어", (15, 15));
            nowPlay = npc1;
            NPCs.Add(npc1);
            NPCs.Add(npc2);
            stage1.SetTile(15, 10, TileType.CCTV, new Computer(Player));
            AddStage(stage2);
            CurrentStage = stage1;
            BossMob boss = new BossMob();
            stage2.SetTile(15, 15, TileType.Boss, boss);
            AddQuestEvent("FindPass", "패스워드를 찾으세요.");
            AddQuestEvent("DefeatBoss", "보스를 처치하세요.");
        }
    }
}
