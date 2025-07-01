using System.Collections.Generic;

namespace ChatEcoSystem.SharedLib.Contracts
{
    /// <summary>
    /// Базовый ответ от API
    /// </summary>
    public class BaseApiResponse<TBody>
    {
        /// <summary>
        /// Статус-код
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public List<string> Errors => new List<string>();

        /// <summary>
        /// Тело ответа
        /// </summary>
        public TBody Body { get; set; } = default;
    }
}
