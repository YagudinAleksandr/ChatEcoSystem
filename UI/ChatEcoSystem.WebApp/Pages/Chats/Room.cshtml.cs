using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatEcoSystem.WebApp.Pages.Chats
{
    public class RoomModel : PageModel
    {
        public string ChatId { get; set; }
        public void OnGet()
        {
            ChatId = Request.Query["id"];
        }
    }
} 