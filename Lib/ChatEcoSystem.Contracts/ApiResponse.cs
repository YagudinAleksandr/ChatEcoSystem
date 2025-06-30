using System.Collections.Generic;

#nullable enable
namespace ChatEcoSystem.Contracts
{
    public class ApiResponse<T> : IApiResponse
    {
        /// <inheritdoc/>
        public int StatusCode { get; set; }

        /// <inheritdoc/>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Тело ответа
        /// </summary>
        public T Body { get; set; } = default!;
    }
}
