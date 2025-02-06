
namespace _15TextRPG.Source.State
{
    public interface IGameState
    {
        void DisplayMenu(GameManager gameManager);
        void HandleInput(GameManager gameManager, string input);
    }
}