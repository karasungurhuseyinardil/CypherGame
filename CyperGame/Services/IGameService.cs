using CyperGame.Models;

namespace CyperGame.Services
{
    public interface IGameService
    {
        GameViewModel CreateNewGame(int? passwordLength = null);
        GameViewModel ProcessGuess(string guess, GameViewModel currentGame);
        GameViewModel GetHint(GameViewModel currentGame);
        GameStatistics GetStatistics(ISession session);
        void UpdateStatistics(ISession session, bool isWon, int guessCount);
    }
}
