using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    public class GameData
    {
        public Player Player { get; set; }
        public FieldData Field { get; set; }
        public BattleData Battle { get; set; }
        public List<StageData> Stages { get; set; }
        public List<Enemy> enemies { get; set; }
        public StageData CurrentStage { get; set; }
        public GameData()
        {
            Player = new Player("Default");
            Field = new FieldData();
            Battle = new BattleData();
            StageData stage1 = new StageData("Stage1", 30, 20);

            for (int x = 0; x < 30; x++)
            {
                stage1.SetTile(x, 0, TileType.Wall);
                stage1.SetTile(x, 19, TileType.Wall);
            }
            for (int y = 0; y < 20; y++)
            {
                stage1.SetTile(0, y, TileType.Wall);
                stage1.SetTile(29, y, TileType.Wall);
            }
            stage1.PlayerStartPosition = (5, 5);
            Field.PositionX = stage1.PlayerStartPosition.x;
            Field.PositionY = stage1.PlayerStartPosition.y;
            stage1.SetTile(10, 10, TileType.Battle, new EnemyTrigger());
            stage1.SetTile(15, 15, TileType.ChangeStage, new ChangeStage("Stage2"));
            stage1.SetTile(20, 15, TileType.Password, new Password("password"));
            stage1.SetTile(12, 12, TileType.NPC, new NPC("무슨일이지?"));

            StageData stage2 = new StageData("Stage2", 30, 20);
            stage2.PlayerStartPosition = (15, 10);
            for (int x = 0; x < 30; x++)
            {
                stage2.SetTile(x, 0, TileType.Wall);
                stage2.SetTile(x, 19, TileType.Wall);
            }
            for (int y = 0; y < 20; y++)
            {
                stage2.SetTile(0, y, TileType.Wall);
                stage2.SetTile(29, y, TileType.Wall);
            }

            stage2.SetTile(5, 5, TileType.Battle, new EnemyTrigger());
            stage2.SetTile(10, 10, TileType.ChangeStage, new ChangeStage("Stage1"));

            Stages = new List<StageData> { stage1, stage2 };
            CurrentStage = stage1;

            enemies = new List<Enemy>()
            {
                new Enemy("Omnic_A", 1, "abc d e f ", 5, 20, 30, new List<Item>()),
                new Enemy("Omnic_B", 1, "abc d e f ", 5, 20, 30, new List<Item>()),
                new Enemy("Omnic_C", 1, "abc d e f ", 5, 20, 30, new List<Item>()),
            };
        }
        public void ChangeStage(string stageName)
        {
            var newStage = Stages.Find(s => s.Name == stageName);
            if (newStage != null)
            {
                CurrentStage = newStage;
                Console.Clear();
                Console.WriteLine($"{stageName}로 이동했습니다.");
            }
        }
    }

    public class FieldData
    {
        //public int[,] Map { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Dir {  get; set; }

        //public FieldData()
        //{
        //    Map = new int[20, 30];
        //}
    }

    public class BattleData
    {

    }

    public class StageData
    {
        public string Name { get; set; }
        public Tile[,] Tiles { get; private set; }
        public (int x, int y) PlayerStartPosition { get; set; }

        public StageData(string name, int width, int height)
        {
            Name = name;
            Tiles = new Tile[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
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

