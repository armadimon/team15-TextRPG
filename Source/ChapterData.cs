﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source.Chapter1;

namespace _15TextRPG.Source
{
    public class ChapterData
    {
        public string Name { get; set; }
        public List<NPC> NPCs { get; set; }
        public NPC? nowPlay { get; set; }
        public List<StageData> Stages { get; private set; }
        public List<QuestItem> QuestItems { get; private set; }
        public StageData? CurrentStage { get; set; }

        public ChapterData(string name)
        {
            Name = name;
            NPCs = new List<NPC>();
            Stages = new List<StageData>();
            QuestItems = new List<QuestItem>();
        }

        public void InitailizeChapter1()
        {
            StageData stage1 = new StageData("Stage1", 30, 20);
            StageData stage2 = new StageData("Stage2", 30, 20);
            AddStage(stage1);
            stage1.SetTile(10, 10, TileType.Battle, new EnemyTrigger());
            stage1.SetTile(29, 5, TileType.ChangeStage, new ChangeStage2("Stage2", false));
            stage1.SetTile(20, 15, TileType.Password, new PasswordForStage1("password"));
            NPC npc1 = new NPC("니콜라스", "메가코프사 경비. 메가코프사의 다양한 '지원'을 받아서 강한 전투력을 얻었지만, 수명은 얼마 남지않았다.", (12, 12));
            NPC npc2 = new NPC("헥터 ", "메가코프사 경비. 메가코프사의 다양한 '지원'을 받아서 강한 전투력을 얻었지만, 수명은 얼마 남지않았다.", (15, 15));
            nowPlay = npc1;
            NPCs.Add(npc1);
            NPCs.Add(npc2);
            stage1.SetTile(12, 12, TileType.NPC, npc1);
            stage1.SetTile(15, 15, TileType.NPC, npc2);
            stage1.SetTile(15, 0, TileType.CCTV, new CCTV());
            AddStage(stage2);
            CurrentStage = stage1;
            BossMob boss = new BossMob();
            stage2.SetTile(15, 15, TileType.Boss, boss);
            AddQuestEvent("FindPass", "패스워드를 찾으세요.");
            AddQuestEvent("DefeatBoss", "보스를 처치하세요.");
        }

        private void InitializeStage(StageData stage)
        {
            for (int x = 0; x < stage.Width; x++)
            {
                stage.SetTile(x, 0, TileType.Wall);
                Console.WriteLine("check   " + x);
                stage.SetTile(x, stage.Height - 1, TileType.Wall);
            }
            for (int y = 0; y < stage.Height; y++)
            {
                stage.SetTile(0, y, TileType.Wall);
                stage.SetTile(stage.Width - 1, y, TileType.Wall);
            }
        }

        public void AddStage(StageData stage)
        {
            Stages.Add(stage);
            InitializeStage(stage);
        }

        public void AddQuestEvent(string questName, string questDesc)
        {
            QuestItems.Add(new QuestItem(questName, questDesc, false));
        }

        public void CompleteQuest(string questName)
        {
            var quest = QuestItems.Find(q => q.Name == questName);
            if (quest != null)
            {
                quest.IsGet = true;
            }
        }
    }

    public class QuestItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsGet { get; set; }

        public QuestItem(string questName, string questDesc, bool isGet)
        {
            Name = questName;
            Description = questDesc;
            IsGet = isGet;
        }
    }

    public class StageData
    {
        public string Name { get; set; }
        public Tile[,] Tiles { get; private set; }
        public (int x, int y) PlayerStartPosition { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public StageData(string name, int width, int height)
        {
            Name = name ?? "";
            Tiles = new Tile[height, width];
            Width = width;
            Height = height;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Tiles[y, x] = new Tile(TileType.Empty);
                }
            }
        }

        public void SetTile(int x, int y, TileType type, IInteractableObject? obj = null)
        {
            Tiles[y, x] = new Tile(type, obj);
        }

        //public void AddQuestEvent(string questName, string questDesc)
        //{
        //    QuestItems.Add(new QuestItem(questName, questDesc, false));
        //}

        //public void SetQuestEvent(string questName)
        //{
        //    for (int i = 0; i < QuestItems.Count; i++)
        //    {
        //        if (QuestItems[i].Name == questName)
        //        {
        //            QuestItems[i].IsGet = true;
        //            break;
        //        }
        //    }
        //}

    }
}

