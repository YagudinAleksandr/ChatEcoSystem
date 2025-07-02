using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatEcoSystem.Auth.Logic;
using ChatEcoSystem.SharedLib.Abstractions;
using ChatEcoSystem.SharedLib.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatEcoSystem.Auth.Api.Controllers
{
    /// <summary>
    /// Контроллер работы с пользователями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        #region CTOR
        /// <inheritdoc cref="IUserService"/>
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="user">Пользователь <see cref="UserCreateDto"/></param>
        [HttpPost(nameof(Create))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseApiResponse<UserDto>))]
        public async Task<IActionResult> Create([FromBody] UserCreateDto user)
        {
            return ActionRes(await _userService.Create(user));
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id">ИД</param>
        [HttpDelete(nameof(Create))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseApiResponse<bool>))]
        public async Task<IActionResult> Delete(Guid id)
        {
            return ActionRes(await _userService.Delete(id));
        }

        /// <summary>
        /// Получение пользователя по ИД
        /// </summary>
        /// <param name="id">ИД</param>
        [HttpGet(nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<UserDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseApiResponse<UserDto>))]
        public async Task<IActionResult> GetById(Guid id)
        {
            return ActionRes(await _userService.GetById(id));
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <param name="filter">Фильтр</param>
        [HttpGet(nameof(GetAll))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<IReadOnlyCollection<UserDto>>))]
        public async Task<IActionResult> GetAll([FromQuery] BaseFilter filter = null)
        {
            return ActionRes(await _userService.GetAll(filter));
        }

        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="user">Данные для обновления <see cref="UserUpdateDto"/></param>
        [HttpPut(nameof(Update))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<UserDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseApiResponse<UserDto>))]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto user)
        {
            return ActionRes(await _userService.Update(user));
        }

        /// <summary>
        /// Обновление пароля пользователя
        /// </summary>
        /// <param name="user">Данные для обновления <see cref="UserUpdatePasswordDto"/></param>
        [HttpPut(nameof(UpdatePassword))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<UserDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseApiResponse<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseApiResponse<UserDto>))]
        public async Task<IActionResult> UpdatePassword([FromBody] UserUpdatePasswordDto user)
        {
            return ActionRes(await _userService.UpdatePassword(user));
        }
    }
}
