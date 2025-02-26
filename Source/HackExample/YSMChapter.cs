﻿using _15TextRPG.Source.Chapter1;
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
            // 스테이지 데이터 생성
            StageData stage1 = new StageData("Stage1", 30, 20);

            AddStage(stage1);

            stage1.SetTile(0, 5, TileType.ChangeStage, new Exit());

            for (int i = 0; i < 29; i++)
            {
                stage1.SetTile(i, 17, TileType.Wall);
            }

            NPC p = new NPC("player", "Free", "당신 그 자체.", "당신은 침묵을 지켰다.", (3, 3));
            NPC npc1 = new NPC("Nicolas", "Stage1", "메가코프의 연구실 직원. 연구 말고 다른 일에는 관심이 없다.", "처음보는 복장인데...", (12, 12));
            NPC npc2 = new NPC("Hector", "Stage1", "보안실 직원. 보안 권한에 대한 비밀번호를 'password'로 해놓는다.", "무슨일이지?", (17, 18));
            nowPlay = p;
            NPCs.Add(p);
            NPCs.Add(npc1);
            NPCs.Add(npc2);
            stage1.SetTile(15, 0, TileType.CCTV, new Computer());

            //스테이지 2 설정
            BossMob boss = new BossMob("Boss");


            // 현재 스테이지 설정
            CurrentStage = stage1;

            // 퀘스트 추가
            Quest killQuest = new Quest("보스를 처치하라", ChapterID.Chapter1, "보스를 처치하세요.", 3, 1500);
            killQuest.Object.Add(new KillEnemyQuest(boss.Name, 1));
            QuestManager.Instance.AddQuest(killQuest);

            AddQuestEvent("FindPass", "패스워드를 찾으세요.");
            AddQuestEvent("DefeatBoss", "보스를 처치하세요.");
        }
    }
}
