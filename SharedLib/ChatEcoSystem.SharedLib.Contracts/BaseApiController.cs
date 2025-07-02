using Microsoft.AspNetCore.Mvc;

namespace ChatEcoSystem.SharedLib.Contracts
{
    /// <summary>
    /// Базовые ответы от сервера
    /// </summary>
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Формирование верного ответа сервера
        /// </summary>
        /// <typeparam name="TTypeResult">Тип данных ответа</typeparam>
        /// <param name="result">Ответ от сервиса</param>
        /// <returns><see cref="IActionResult"/></returns>
        public IActionResult ActionRes<TTypeResult>(BaseApiResponse<TTypeResult> result)
        {
            if (result == null)
                return NotFound();

            return result.StatusCode switch
            {
                200 => Ok(result),
                201 => StatusCode(201, result),
                204 => NoContent(),
                400 => BadRequest(result),
                401 => Unauthorized(),
                403 => Forbid(),
                404 => NotFound(result),
                409 => Conflict(result),
                422 => UnprocessableEntity(result),
                500 => StatusCode(500, result),
                _ => StatusCode(result.StatusCode, result)
            };
        }
    }
}
