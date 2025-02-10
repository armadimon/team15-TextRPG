using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using _15TextRPG.Source.Chapter1;

namespace _15TextRPG.Source.State
{
    public class ExploreState : IGameState
    {
        public string StageName { get; set; }
        public ExploreState(string stageName)
        {
            StageName = stageName;
        }

        public void DisplayMenu(GameManager gameManager)
        {
            Console.Clear();
            DisplayMap(gameManager.GameData);
        }

        public void HandleInput(GameManager gameManager)
        {
            NPC? npc = gameManager.GameData.CurrentChapter.nowPlay;
            if (npc == null)
            {
                Console.WriteLine("nowPlay is null");
                return;
            }
            StageData? stage = gameManager.GameData.CurrentChapter.CurrentStage;
            if (stage == null)
            {
                Console.WriteLine("CurrentStage is null");
                return;
            }
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (npc.posY - 1 < stage.Height - 1 && npc.posY - 1 > 0
                        && stage.Tiles[npc.posY - 1, npc.posX].Type == TileType.Empty)
                    {
                        stage.SetTile(npc.posX, npc.posY, TileType.Empty);
                        npc.posY -= 1;
                        stage.SetTile(npc.posX, npc.posY, TileType.NPC, npc);
                    }
                    npc.Dir = 1;
                    break;
                case ConsoleKey.DownArrow:
                    if (npc.posY + 1 < stage.Height - 1 && npc.posY + 1 > 0
                         && stage.Tiles[npc.posY + 1, npc.posX].Type == TileType.Empty)
                    {
                        stage.SetTile(npc.posX, npc.posY, TileType.Empty);
                        npc.posY += 1;
                        stage.SetTile(npc.posX, npc.posY, TileType.NPC, npc);
                    }
                    npc.Dir = 2;
                    break;
                case ConsoleKey.LeftArrow:
                    if (npc.posX - 1 < stage.Width - 1 && npc.posX - 1 > 0
                         && stage.Tiles[npc.posY, npc.posX - 1].Type == TileType.Empty)
                    {
                        stage.SetTile(npc.posX, npc.posY, TileType.Empty);
                        npc.posX -= 1;
                        stage.SetTile(npc.posX, npc.posY, TileType.NPC, npc);
                    }
                    npc.Dir = 3;
                    break;
                case ConsoleKey.RightArrow:
                    if (npc.posX + 1 < stage.Width - 1 && npc.posX + 1 > 0
                         && stage.Tiles[npc.posY, npc.posX + 1].Type == TileType.Empty)
                    {
                        stage.SetTile(npc.posX, npc.posY, TileType.Empty);
                        npc.posX += 1;
                        stage.SetTile(npc.posX, npc.posY, TileType.NPC, npc);
                    }
                    npc.Dir = 4;
                    break;
                case ConsoleKey.Z:
                    HandleInteraction(gameManager);
                    break;
            }
        }

        private void HandleInteraction(GameManager gameManager)
        {
            ChapterData chapter = gameManager.GameData.CurrentChapter;
            NPC? npc = gameManager.GameData.CurrentChapter.nowPlay;
            if (npc == null)
            {
                Console.WriteLine("nowPlay is null");
                return;
            }
            if (chapter.CurrentStage == null)
            {
                Console.WriteLine("CurrentStage is null");
                return;
            }
            (int x, int y) targetPos;
            switch (npc.Dir)
            {
                case 1: // 위쪽
                    targetPos = (npc.posX, npc.posY - 1);
                    break;
                case 2: // 아래쪽
                    targetPos = (npc.posX, npc.posY + 1);
                    break;
                case 3: // 왼쪽
                    targetPos = (npc.posX - 1, npc.posY);
                    break;
                case 4: // 오른쪽
                    targetPos = (npc.posX + 1, npc.posY);
                    break;
                default:
                    targetPos = (npc.posX, npc.posY);
                    break;
            }

            Tile targetTile = chapter.CurrentStage.Tiles[targetPos.y, targetPos.x];

            if (targetTile.Object != null)
            {
                targetTile.Object.Interact(gameManager);
            }
            else
            {
                Console.WriteLine("여기에는 아무것도 없습니다.");
            }
        }

        public void DisplayMap(GameData gameData)
        {
            StageData stage = gameData.CurrentChapter.CurrentStage;
            (int px, int py) = (gameData.CurrentChapter.nowPlay.posX, gameData.CurrentChapter.nowPlay.posY);

            for (int y = 0; y < stage.Tiles.GetLength(0); y++)
            {
                for (int x = 0; x < stage.Tiles.GetLength(1); x++)
                {
                    char displayChar = GetTileChar(stage.Tiles[y, x]);
                    if (x == px && y == py)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('P');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(displayChar);
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        private char GetTileChar(Tile tile)
        {
            switch (tile.Type)
            {
                case TileType.Wall:
                    return '#';
                case TileType.Boss:
                    return 'B';
                case TileType.Empty:
                    return ' ';
                case TileType.ChangeStage:
                    return 'O';
                case TileType.Battle:
                    return 'E';
                case TileType.Password:
                    return '*';
                case TileType.NPC:
                    return 'N';
                default:
                    return '?';
            }
        }
    }
}
