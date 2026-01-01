namespace CyperGame.Models
{
    public enum LetterStatus
    {
        Correct,
        WrongPlace,
        Absent
    }

    public class LetterGuess
    {
        public char Character { get; set; }
        public LetterStatus Status { get; set; }
    }
}
