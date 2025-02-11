using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _15TextRPG.Source.State
{
    internal class Computer : IHackable, IInteractableObject
    {
        public HackState HackState { get; }
        public Player Player { get; }

        public Computer(Player player)
        {
        }

        public bool HackingProcess(string userCommand)
        {
            string[] command = userCommand.Split(' ');
            switch(command[0])
            {
                case "item":
                    if(command.Count() == 1)
                    {
                        Console.Write(Player.Inventory);
                    }
                    else
                    {
                        Player.Inventory.Use(command[1]);
                        Player.Inventory.Subtract(command[1]);
                    }
                    break;
                    
                default:

                    Console.Write("없는 명령어입니다.");
                    break;
            }
            return false;
        }

        public void Interact(GameManager gameManager)
        {
            gameManager.ChangeState(HackState);
        }
    }
}
