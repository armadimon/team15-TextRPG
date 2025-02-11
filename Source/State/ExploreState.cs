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

        public void DisplayMenu()
        {
            Console.Clear();
            DisplayMap(GameManager.Instance.GameData, GameManager.Instance.GameData.CurrentChapter.CurrentStage);
        }

        public void HandleInput()
        {
            NPC? npc = GameManager.Instance.GameData.CurrentChapter.nowPlay;
            Console.WriteLine($"check : {(npc.posX, npc.posY - 1)}");
            if (npc == null)
            {
                Console.WriteLine("nowPlay is null");
                return;
            }
            StageData? stage = GameManager.Instance.GameData.CurrentChapter.CurrentStage;
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
                case ConsoleKey.Q:
                    Console.WriteLine($"check : {(npc.posX, npc.posY - 1)}");
                    HackingMode();
                    break;
                case ConsoleKey.Z:
                    Console.WriteLine($"check : {(npc.posX, npc.posY - 1)}");
                    HandleInteraction();
                    break;
            }
        }

        private void HackingMode()
        {
            List<NPC> npcs = GameManager.Instance.GameData.CurrentChapter.NPCs;
            int i = 0;

            void DrawNpcInfo()
            {
                Console.Clear();
                DisplayMap(GameManager.Instance.GameData, GameManager.Instance.GameData.CurrentChapter.CurrentStage);
                Console.WriteLine("해킹 모드로 전환합니다.");
                Console.WriteLine("키보드 좌우 화살표로 목표를 전환. 'Z' 키를 눌러 결정하세요. 나가기 : ESC");
                Console.SetCursorPosition(npcs[i].posX, npcs[i].posY);
                Console.WriteLine($"{npcs[i].Name}: {npcs[i].Desc} ({npcs[i].posX}, {npcs[i].posY})");
            }

            DrawNpcInfo();

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (i > 0) i--;
                        DrawNpcInfo();
                        break;
                    case ConsoleKey.RightArrow:
                        if (i < npcs.Count - 1) i++;
                        DrawNpcInfo();
                        break;
                    case ConsoleKey.Z:
                        if (npcs[i].Name == "player" || npcs[i].IsHacked == true)
                        {
                            GameManager.Instance.GameData.CurrentChapter.nowPlay = npcs[i];
                        }
                        else
                            GameManager.Instance.ChangeState(new JHNCombatState(npcs[i]));
                        return;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }


        private void HandleInteraction()
        {
            ChapterData chapter = GameManager.Instance.GameData.CurrentChapter;
            NPC? npc = GameManager.Instance.GameData.CurrentChapter.nowPlay;
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
                targetTile.Object.Interact();
            }
            else
            {
                Console.WriteLine("여기에는 아무것도 없습니다.");
            }
        }

        public void CCTVMode()
        {
            List<StageData> stages = GameManager.Instance.GameData.CurrentChapter.Stages;

            ConsoleKeyInfo keyInfo;
            int i = 0;
            do
            {
                Console.WriteLine($"{i}층 CCTV");
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (i > stages.Count - 1)
                            i = 0;
                        DisplayMap(GameManager.Instance.GameData, stages[i]);
                        i++;
                        break;
                    case ConsoleKey.RightArrow:
                        if (i > stages.Count - 1)
                            i = 0;
                        DisplayMap(GameManager.Instance.GameData, stages[i]);
                        i++;
                        break;
                }
            } while (keyInfo.Key != ConsoleKey.Escape);
        }


        public void DisplayMap(GameData gameData, StageData stage)
        {
            (int px, int py) = (gameData.CurrentChapter.nowPlay.posX, gameData.CurrentChapter.nowPlay.posY);

            for (int y = 0; y < stage.Tiles.GetLength(0); y++)
            {
                for (int x = 0; x < stage.Tiles.GetLength(1); x++)
                {
                    char displayChar = GetTileChar(stage.Tiles[y, x]);
                    if (x == px && y == py && stage.Name == gameData.CurrentChapter.CurrentStage.Name)
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
                case TileType.CCTV:
                    return 'C';
                default:
                    return '?';
            }
        }
    }
}
