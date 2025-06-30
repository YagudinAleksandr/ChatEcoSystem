using System.Collections.Generic;

namespace ChatEcoSystem.Contracts
{
    /// <summary>
    /// Базовый ответ сервера
    /// </summary>
    public interface IApiResponse
    {
        /// <summary>
        /// Статус-код ответа сервера
        /// </summary>
        int StatusCode { get; set; }

        /// <summary>
        /// Список ошибок
        /// </summary>
        List<string> Errors { get; set; }
    }
}
