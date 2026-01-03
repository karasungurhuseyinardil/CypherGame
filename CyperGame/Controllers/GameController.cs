using Microsoft.AspNetCore.Mvc;
using CyperGame.Models;
using CyperGame.Services;
using CyperGame.Helpers;

namespace CyperGame.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var game = HttpContext.Session.GetGame();
            
            if (game == null)
            {
                game = _gameService.CreateNewGame();
                game.ShowWelcome = true;
                HttpContext.Session.SetGame(game);
            }

            return View(game);
        }

        [HttpPost]
        public IActionResult Guess(string userGuess)
        {
            var game = HttpContext.Session.GetGame();
            if (game == null || game.IsGameOver)
            {
                return RedirectToAction("Index");
            }

            userGuess = userGuess?.ToUpper(new System.Globalization.CultureInfo("tr-TR"))?.Trim() ?? "";

            if (!System.Text.RegularExpressions.Regex.IsMatch(userGuess, "^[A-Z0-9ÇĞİÖŞÜ]*$"))
            {
                game.UserGuess = userGuess;
                game.Message = "Lütfen sadece harf ve rakam kullanın! Noktalama işareti giremezsiniz.";
                return View("Index", game);
            }

            if (userGuess.Length != game.PasswordLength)
            {
                game.UserGuess = userGuess;
                game.Message = $"Tahmininiz {game.PasswordLength} karakter olmalıdır!";
                return View("Index", game);
            }

            game = _gameService.ProcessGuess(userGuess, game);
            HttpContext.Session.SetGame(game);

            if (game.IsGameOver)
            {
                var guessCount = game.TotalGuesses - game.RemainingGuesses;
                _gameService.UpdateStatistics(HttpContext.Session, game.IsWon, guessCount);
            }

            return View("Index", game);
        }

        [HttpPost]
        public IActionResult GetHint()
        {
            var game = HttpContext.Session.GetGame();
            if (game == null || game.IsGameOver)
            {
                return RedirectToAction("Index");
            }

            game = _gameService.GetHint(game);
            HttpContext.Session.SetGame(game);

            if (game.IsGameOver)
            {
                _gameService.UpdateStatistics(HttpContext.Session, false, game.TotalGuesses);
            }

            return View("Index", game);
        }

        [HttpPost]
        public IActionResult NewGame()
        {
            HttpContext.Session.ClearGame();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Statistics()
        {
            var stats = _gameService.GetStatistics(HttpContext.Session);
            return PartialView("_StatisticsPartial", stats);
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}
