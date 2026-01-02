using CyperGame.Models;
using System.Text.Json;

namespace CyperGame.Helpers
{
    public static class SessionExtensions
    {
        private const string GameKey = "CurrentGame";

        public static void SetGame(this ISession session, GameViewModel game)
        {
            session.SetString(GameKey, JsonSerializer.Serialize(game));
        }

        public static GameViewModel? GetGame(this ISession session)
        {
            var gameJson = session.GetString(GameKey);
            if (string.IsNullOrEmpty(gameJson)) return null;
            return JsonSerializer.Deserialize<GameViewModel>(gameJson);
        }

        public static void ClearGame(this ISession session)
        {
            session.Remove(GameKey);
        }
    }
}
