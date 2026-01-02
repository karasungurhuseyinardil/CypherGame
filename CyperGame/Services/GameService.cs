using CyperGame.Models;
using CyperGame.Helpers;
using System.Text.Json;

namespace CyperGame.Services
{
    public class GameService : IGameService
    {
        private const string StatsKey = "PlayerStatistics";

        public GameViewModel CreateNewGame(int? passwordLength = null)
        {
            var random = new Random();
            int length = passwordLength ?? random.Next(5, 8);
            string password = PasswordHelper.GeneratePassword(length);
            
            return new GameViewModel
            {
                TargetPassword = password,
                PasswordLength = password.Length,
                TotalGuesses = password.Length + 5,
                RemainingGuesses = password.Length + 5,
                HintCount = 0,
                ShowWelcome = false,
                IsGameOver = false,
                IsWon = false,
                Message = "Yeni oyun başladı. Şifreyi tahmin edin!",
                HintMessages = new List<string>(),
                PreviousGuesses = new List<List<LetterGuess>>()
            };
        }

        public GameViewModel ProcessGuess(string guess, GameViewModel currentGame)
        {
            var guessResults = CalculateGuessResults(guess, currentGame.TargetPassword);
            
            currentGame.PreviousGuesses ??= new List<List<LetterGuess>>();
            currentGame.PreviousGuesses.Insert(0, guessResults);
            currentGame.LastGuessResults = guessResults;
            currentGame.UserGuess = guess;

            if (guess == currentGame.TargetPassword)
            {
                currentGame.IsWon = true;
                currentGame.IsGameOver = true;
                currentGame.Message = "Tebrikler! Şifreyi doğru bildiniz.";
            }
            else
            {
                currentGame.RemainingGuesses--;
                
                if (currentGame.RemainingGuesses <= 0)
                {
                    currentGame.IsGameOver = true;
                    currentGame.IsWon = false;
                    currentGame.Message = $"Oyun Bitti! Maalesef bilemediniz. Doğru şifre: {currentGame.TargetPassword}";
                }
                else
                {
                    currentGame.Message = "Yanlış tahmin, tekrar deneyin.";
                }
            }

            return currentGame;
        }

        public GameViewModel GetHint(GameViewModel currentGame)
        {
            if (currentGame.HintCount >= GameViewModel.MaxHints || currentGame.RemainingGuesses <= 0)
            {
                return currentGame;
            }

            currentGame.RemainingGuesses--;
            currentGame.HintCount++;

            var hintHistory = currentGame.HintMessages ?? new List<string>();
            var usedIndices = new List<int>();
            
            foreach (var hint in hintHistory)
            {
                var indexStr = hint.Split('.')[0].Replace("İpucu: ", "");
                if (int.TryParse(indexStr, out int idx))
                {
                    usedIndices.Add(idx - 1);
                }
            }

            var availableIndices = Enumerable.Range(0, currentGame.TargetPassword.Length)
                                            .Where(i => !usedIndices.Contains(i))
                                            .ToList();

            if (availableIndices.Any())
            {
                var random = new Random();
                int index = availableIndices[random.Next(availableIndices.Count)];
                char hintChar = currentGame.TargetPassword[index];
                string newHint = $"İpucu: {index + 1}. karakter '{hintChar}'";
                hintHistory.Add(newHint);
                currentGame.HintMessages = hintHistory;
            }

            if (currentGame.RemainingGuesses <= 0)
            {
                currentGame.IsGameOver = true;
                currentGame.IsWon = false;
                currentGame.Message = $"Oyun Bitti! İpucu alırken hakkınız tükendi. Doğru şifre: {currentGame.TargetPassword}";
            }

            return currentGame;
        }

        public GameStatistics GetStatistics(ISession session)
        {
            var statsJson = session.GetString(StatsKey);
            if (string.IsNullOrEmpty(statsJson))
            {
                return new GameStatistics();
            }
            return JsonSerializer.Deserialize<GameStatistics>(statsJson) ?? new GameStatistics();
        }

        public void UpdateStatistics(ISession session, bool isWon, int guessCount)
        {
            var stats = GetStatistics(session);
            
            stats.GamesPlayed++;
            
            if (isWon)
            {
                stats.GamesWon++;
                stats.CurrentStreak++;
                if (stats.CurrentStreak > stats.MaxStreak)
                {
                    stats.MaxStreak = stats.CurrentStreak;
                }
                
                if (!stats.GuessDistribution.ContainsKey(guessCount))
                {
                    stats.GuessDistribution[guessCount] = 0;
                }
                stats.GuessDistribution[guessCount]++;
            }
            else
            {
                stats.CurrentStreak = 0;
            }

            session.SetString(StatsKey, JsonSerializer.Serialize(stats));
        }

        private List<LetterGuess> CalculateGuessResults(string guess, string target)
        {
            var results = new List<LetterGuess>();
            var targetChars = target.ToCharArray().ToList();
            var guessChars = guess.ToCharArray();

            for (int i = 0; i < guessChars.Length; i++)
            {
                results.Add(new LetterGuess { Character = guessChars[i], Status = LetterStatus.Absent });
            }

            for (int i = 0; i < guessChars.Length; i++)
            {
                if (i < targetChars.Count && guessChars[i] == targetChars[i])
                {
                    results[i].Status = LetterStatus.Correct;
                    targetChars[i] = '\0';
                }
            }

            for (int i = 0; i < guessChars.Length; i++)
            {
                if (results[i].Status != LetterStatus.Correct)
                {
                    int index = targetChars.IndexOf(guessChars[i]);
                    if (index != -1 && targetChars[index] != '\0')
                    {
                        results[i].Status = LetterStatus.WrongPlace;
                        targetChars[index] = '\0';
                    }
                }
            }

            return results;
        }
    }
}
