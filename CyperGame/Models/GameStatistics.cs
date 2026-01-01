namespace CyperGame.Models
{
    public class GameStatistics
    {
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int CurrentStreak { get; set; }
        public int MaxStreak { get; set; }
        public Dictionary<int, int> GuessDistribution { get; set; } = new Dictionary<int, int>();
        
        public double WinPercentage => GamesPlayed > 0 ? Math.Round((double)GamesWon / GamesPlayed * 100, 1) : 0;
        
        public int AverageGuesses
        {
            get
            {
                if (GuessDistribution.Count == 0) return 0;
                var total = GuessDistribution.Sum(x => x.Key * x.Value);
                var count = GuessDistribution.Sum(x => x.Value);
                return count > 0 ? (int)Math.Round((double)total / count) : 0;
            }
        }
    }
}
