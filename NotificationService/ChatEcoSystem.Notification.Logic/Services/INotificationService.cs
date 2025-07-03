using System.Threading.Tasks;

namespace ChatEcoSystem.Notification.Logic
{
    /// <summary>
    /// Сервис обработки уведомлений
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Проверить и отправить уведомления о непрочитанных сообщениях
        /// </summary>
        Task ProcessNotificationsAsync();
    }
}
