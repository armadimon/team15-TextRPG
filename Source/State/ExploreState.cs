using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _15TextRPG.Source.State
{
    internal class ExploreState : IGameState
    {
        public string StageName { get; set; }
        public ExploreState(string stageName)
        {
            stageName = "stage1";
        }

        public void DisplayMenu(GameManager gameManager)
        {
            Console.Clear();
            DisplayMap(gameManager.GameData);
        }

        public void HandleInput(GameManager gameManager)
        {
            FieldData field = gameManager.GameData.Field;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    field.PositionY -= 1;
                    field.Dir = 1;
                    break;
                case ConsoleKey.DownArrow:
                    field.PositionY += 1;
                    field.Dir = 2;
                    break;
                case ConsoleKey.LeftArrow:
                    field.PositionX -= 1;
                    field.Dir = 3;
                    break;
                case ConsoleKey.RightArrow:
                    field.PositionX += 1;
                    field.Dir = 4;
                    break;
                case ConsoleKey.Z:
                    HandleInteraction(gameManager);
                    break;
            }
        }

        private void HandleInteraction(GameManager gameManager)
        {
            FieldData field = gameManager.GameData.Field;
            StageData stage = gameManager.GameData.CurrentStage;

            (int x, int y) targetPos;
            switch (field.Dir)
            {
                case 1: // 위쪽
                    targetPos = (field.PositionX, field.PositionY - 1);
                    break;
                case 2: // 아래쪽
                    targetPos = (field.PositionX, field.PositionY + 1);
                    break;
                case 3: // 왼쪽
                    targetPos = (field.PositionX - 1, field.PositionY);
                    break;
                case 4: // 오른쪽
                    targetPos = (field.PositionX + 1, field.PositionY);
                    break;
                default:
                    targetPos = (field.PositionX, field.PositionY);
                    break;
            }

            Tile targetTile = stage.Tiles[targetPos.y, targetPos.x];

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
            StageData stage = gameData.CurrentStage;
            (int px, int py) = (gameData.Field.PositionX, gameData.Field.PositionY);

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
            return tile.Type switch
            {
                TileType.Wall => '#',
                TileType.Empty => ' ',
                TileType.ChangeStage => 'O',
                TileType.Battle => 'E',
                TileType.Password => '*',
                TileType.NPC => 'N',
                _ => '?'
            };
        }
    }
}
