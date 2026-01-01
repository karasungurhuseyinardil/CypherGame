namespace CyperGame.Models
{
    public class GameViewModel
    {
        public string? TargetPassword { get; set; }
        public int PasswordLength { get; set; }
        public int TotalGuesses { get; set; }
        public int RemainingGuesses { get; set; }
        public string? UserGuess { get; set; }
        public string? Message { get; set; }
        public List<string> HintMessages { get; set; } = new List<string>();
        public int HintCount { get; set; }
        public const int MaxHints = 3;
        public bool ShowWelcome { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsWon { get; set; }
        public List<LetterGuess>? LastGuessResults { get; set; }
        public List<List<LetterGuess>>? PreviousGuesses { get; set; }
    }
}
