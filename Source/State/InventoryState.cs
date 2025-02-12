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

            Console.WriteLine("\n1. 아이템 추가 ㅌㅅㅌ");
            Console.WriteLine("\n2. 아이템 사용");
            Console.WriteLine("\n0. 나가기");
        }

        public void HandleInput()
        {
            var item = ItemData.RecoveryItems[0];

            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
            string input = Console.ReadLine() ?? "";
            switch (input)
            {
                case "1":
                    GameManager.Instance.GameData.Player.Inventory.Add(item);
                    Console.WriteLine("추가됨");
                    break;
                case "2":     
                    if (GameManager.Instance.GameData.Player.Inventory.Find(item) != null)
                    {
                        GameManager.Instance.GameData.Player.Inventory.Use(item, GameManager.Instance.GameData.Player);
                        Console.WriteLine("아이템 사용함");
                        GameManager.Instance.GameData.Player.Inventory.Subtract(item);
                        Console.WriteLine("아이템 삭제함");
                    }
                    else if(GameManager.Instance.GameData.Player.Health < GameManager.Instance.GameData.Player.MaxHP)
                    {
                        Console.WriteLine("플레이어의 체력이 이미 가득 차 있습니다.");
                    }
                    else
                    {
                        Console.WriteLine("아이템이 없습니다.");
                    }

                    Console.ReadLine();
                    break;
                case "0":
                    GameManager.Instance.ChangeState(new MainMenuState());
                    break;
            }
        }
    }
}
