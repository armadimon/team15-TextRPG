using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source.Chapter1;

namespace _15TextRPG.Source
{
    public enum ChapterID
    {
        Chapter1
    }

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
            // 스테이지 데이터 생성
            StageData stage1 = new StageData("Stage1", 30, 20);
            StageData stage2 = new StageData("Stage2", 30, 20);

            AddStage(stage1);
            AddStage(stage2);

            //스테이지 1 설정
            stage1.SetTile(10, 10, TileType.Battle, new EnemyTrigger((10, 10)));
            stage1.SetTile(29, 5, TileType.ChangeStage, new ChangeStage(stage2, false));
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

            stage1.SetTile(3, 3, TileType.NPC, p);
            stage1.SetTile(npc1.posX, npc1.posY, TileType.NPC, npc1);
            stage1.SetTile(npc2.posX, npc2.posY, TileType.NPC, npc2);
            stage1.SetTile(2, 18, TileType.Password, new PasswordForStage1("password"));
            stage1.SetTile(15, 0, TileType.CCTV, new CCTV());

            //스테이지 2 설정
            for (int i = 3; i < 29; i++)
            {
                stage2.SetTile(i, 4, TileType.Wall);
            }
            for (int i = 3; i < 29; i++)
            {
                stage2.SetTile(i, 6, TileType.Wall);
            }
            stage2.SetTile(1, 3, TileType.Wall);
            stage2.SetTile(2, 3, TileType.Wall);
            stage2.SetTile(3, 3, TileType.Wall);
            stage2.SetTile(1, 7, TileType.Wall);
            stage2.SetTile(2, 7, TileType.Wall);
            stage2.SetTile(3, 7, TileType.Wall);
            Database database = new Database();
            stage2.SetTile(2, 5, TileType.ETC, database);
            //stage2.SetTile(5, 5, TileType.Battle, new EnemyTrigger((5, 5)));
            stage2.SetTile(29, 5, TileType.ChangeStage, new ChangeStage(stage1, true));

            // 현재 스테이지 설정
            CurrentStage = stage1;

            // 퀘스트 추가
            //Quest killQuest = new Quest("보스를 처치하라", ChapterID.Chapter1, "보스를 처치하세요.");
            //killQuest.Object.Add(new KillEnemyQuest(boss.Name, 1));
            //QuestManager.Instance.AddQuest(killQuest);


            Quest collectQuest = new Quest("기밀 문서를 입수하라", ChapterID.Chapter1, "메가코프의 최상층에서 기밀 문서를 입수해라.", 5, 3000);
            collectQuest.Object.Add(new CollectItemQuest("기밀문서", 1));
            QuestManager.Instance.AddQuest(collectQuest);

            Quest HackQuest = new Quest("해킹으로 정보를 얻어라", ChapterID.Chapter1, "해킹을 2번 성공 시켜라", 3, 1500);
            HackQuest.Object.Add(new HackEnemyQuest(2));
            QuestManager.Instance.AddQuest(HackQuest);

            AddQuestEvent("FindPass", "패스워드를 찾으세요.");
            //AddQuestEvent("GetSecret", "기밀을 획득하세요.");
        }

        private void InitializeStage(StageData stage)
        {
            for (int x = 0; x < stage.Width; x++)
            {
                stage.SetTile(x, 0, TileType.Wall);
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

    }
}

