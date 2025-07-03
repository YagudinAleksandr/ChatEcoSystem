using System.Threading.Tasks;

namespace ChatEcoSystem.Notification.Logic
{
    /// <summary>
    /// Сервис отправки email уведомлений
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Отправить уведомление по email
        /// </summary>
        /// <param name="email">Email получателя</param>
        /// <param name="subject">Тема письма</param>
        /// <param name="body">Текст письма</param>
        Task SendNotificationAsync(string email, string subject, string body);
    }
}
