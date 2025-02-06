using System;

<<<<<<< Updated upstream
public class StatusMenuState : IGameState
{
    public void DisplayMenu(GameManager gameManager)
    {
        Console.Clear();
        gameManager.Player.ShowInventory();
    }

    public void HandleInput(GameManager gameManager, string input)
    {
        switch (input)
        {
            case "0":
                gameManager.ChangeState(new MainMenuState());
                break;
=======
namespace _15TextRPG.Source.State
{
    public class StatusMenuState : IGameState
    {
        public void DisplayMenu(GameManager gameManager)
        {
            Console.Clear();
            gameManager.Player.ShowStatus();
        }

        public void HandleInput(GameManager gameManager, string input)
        {
            switch (input)
            {
                case "0":
                    gameManager.ChangeState(new MainMenuState());
                    break;
            }
>>>>>>> Stashed changes
        }
    }
}