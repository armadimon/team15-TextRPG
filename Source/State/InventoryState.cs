using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.State
{

    public class InventoryState : IGameState
    {
        public void DisplayMenu()
        {
            Console.Clear();
            GameManager.Instance.GameData.Player.Inventory.ShowInventory(); 
            Console.WriteLine("\n1. 아이템 사용");
            Console.WriteLine("\n9. 아이템 추가(테스트용)");
            Console.WriteLine("\n0. 나가기");
        }

        public void HandleInput()
        {
            var recItem = ItemData.DropItems[0];
            var othertem = ItemData.DropItems[1];

            var strItem = ItemData.StateUpgradeItems[0];


            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
            string input = Console.ReadLine() ?? "";
            switch (input)
            {
                case "9":
                    GameManager.Instance.GameData.Player.Inventory.Add(recItem);
                    GameManager.Instance.GameData.Player.Inventory.Add(strItem);
                    GameManager.Instance.GameData.Player.Inventory.Add(othertem);


                    Console.WriteLine("아이템이 추가됐습니다");
                    break;
                case "1":
                    if (GameManager.Instance.GameData.Player.Inventory.Count == 0)
                    {
                        Console.Write("\n아이템이 존재하지 않습니다.\n 아무 키를 눌러주세요.\n>>");
                        Console.ReadLine();
                        return;

                    }
                    Console.Write("\n원하시는 아이템을 입력해주세요.\n>> ");
                    string input1 = Console.ReadLine() ?? "";

                    if (!int.TryParse(input1, out int i) || i > GameManager.Instance.GameData.Player.Inventory.Count || i <= 0)
                    {
                        Console.WriteLine("잘못된 입력입니다.\n");
                        return;
                    }
                    else
                    {
                        if (GameManager.Instance.GameData.Player.Inventory.Items[i-1].Count != 0)
                        {
                            Console.WriteLine($"{GameManager.Instance.GameData.Player.Inventory.GetItemName(i - 1)}을 사용하였습니다.");
                            GameManager.Instance.GameData.Player.Inventory.Use(GameManager.Instance.GameData.Player.Inventory.GetItemName(i - 1));
                            GameManager.Instance.GameData.Player.Inventory.Subtract(GameManager.Instance.GameData.Player.Inventory.GetItemName(i - 1));
                        }
                        else
                        {
                            Console.WriteLine($"{GameManager.Instance.GameData.Player.Inventory.GetItemName(i - 1)}(이)가 없습니다.");
                        }
                    }

                    //if (GameManager.Instance.GameData.Player.Inventory.Find(recItem) != null)
                    //{
                    //    GameManager.Instance.GameData.Player.Inventory.Use(recItem, GameManager.Instance.GameData.Player);
                    //    Console.WriteLine("아이템 사용함");
                    //    GameManager.Instance.GameData.Player.Inventory.Subtract(recItem);
                    //    Console.WriteLine("아이템 삭제함");
                    //}
                    //else if(GameManager.Instance.GameData.Player.Health < GameManager.Instance.GameData.Player.MaxHP)
                    //{
                    //    Console.WriteLine("플레이어의 체력이 이미 가득 차 있습니다.");
                    //}
                    //else
                    //{
                    //    Console.WriteLine("아이템이 없습니다.");
                    //}

                    Console.ReadLine();
                    break;
                case "0":
                    GameManager.Instance.ChangeState(new MainMenuState());
                    break;
            }
        }
    }
}
