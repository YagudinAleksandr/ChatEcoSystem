using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChatEcoSystem.WebApp.Services;

namespace ChatEcoSystem.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiService _apiService;
        public AccountController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var response = await _apiService.LoginAsync(new { Username = username, Password = password });
            if (response.IsSuccessStatusCode)
            {
                // Здесь можно обработать токен и т.д.
                return RedirectToAction("Index", "Chat");
            }
            ModelState.AddModelError("", "Ошибка авторизации");
            return View();
        }
    }
} 