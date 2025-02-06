using System;

public class MainMenuState : IGameState
{
    public void DisplayMenu(GameManager gameManager)
    {
        Console.Clear();
        Console.WriteLine("\n[ķ�� �Ա�]");
        Console.WriteLine("\n1. ���� ����");
        Console.WriteLine("0. ����");
    }

    public void HandleInput(GameManager gameManager, string input)
    {
        switch (input)
        {
            case "1":
                gameManager.ChangeState(new StatusMenuState());
                break;
            case "0":
                gameManager.QuitGame();
                break;
        }
    }
}