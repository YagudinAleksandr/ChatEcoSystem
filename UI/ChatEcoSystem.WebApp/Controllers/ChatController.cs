using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChatEcoSystem.WebApp.Services;

namespace ChatEcoSystem.WebApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly ApiService _apiService;
        public ChatController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _apiService.GetChatRoomsAsync();
            if (response.IsSuccessStatusCode)
            {
                var chatRooms = await response.Content.ReadAsStringAsync(); // Для примера, обычно десериализация
                ViewBag.ChatRooms = chatRooms;
            }
            else
            {
                ViewBag.ChatRooms = "Ошибка загрузки чатов";
            }
            return View();
        }
    }
} 