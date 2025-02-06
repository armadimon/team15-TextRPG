public interface IGameState
{
    void DisplayMenu(GameManager gameManager);
    void HandleInput(GameManager gameManager, string input);
}
