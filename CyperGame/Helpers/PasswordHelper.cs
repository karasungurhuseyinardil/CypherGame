using System.Text;

namespace CyperGame.Helpers
{
    public static class PasswordHelper
    {
        private static readonly string Letters = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";
        private static readonly string Numbers = "0123456789";

        public static string GeneratePassword(int length)
        {
            var random = new Random();
            var combined = Letters + Numbers;
            var password = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                password.Append(combined[random.Next(combined.Length)]);
            }

            return password.ToString();
        }
    }
}
