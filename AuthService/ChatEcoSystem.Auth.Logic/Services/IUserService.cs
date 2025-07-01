using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatEcoSystem.SharedLib.Abstractions;
using ChatEcoSystem.SharedLib.Contracts;

namespace ChatEcoSystem.Auth.Logic
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="userCreate">Пользователь <see cref="UserCreateDto"/></param>
        /// <returns>Созданный пользователь <see cref="UserDto"/></returns>
        Task<BaseApiResponse<UserDto>> Create(UserCreateDto userCreate);

        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="userUpdate">Пользователь <see cref="UserUpdateDto"/></param>
        /// <returns>Обновленный пользователь <see cref="UserDto"/></returns>
        Task<BaseApiResponse<UserDto>> Update(UserUpdateDto userUpdate);

        /// <summary>
        /// Обновление пароля
        /// </summary>
        /// <param name="userUpdatePassword">Пользователь с обновленным паролем <see cref="UserUpdatePasswordDto"/></param>
        /// <returns>Обновленный пользователь</returns>
        Task<BaseApiResponse<UserDto>> UpdatePassword(UserUpdatePasswordDto userUpdatePassword);

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns>true - удален, false - не удален</returns>
        Task<BaseApiResponse<bool>> Delete(Guid id);

        /// <summary>
        /// Получение пользователя по ИД
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns>Пользователь <see cref="UserDto"/></returns>
        Task<BaseApiResponse<UserDto>> GetById(Guid id);

        /// <summary>
        /// Проверка на существование E-mail
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <returns>true - существует, false - отсутствует</returns>
        Task<BaseApiResponse<bool>> ExistEmail(string email);

        /// <summary>
        /// Получение по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Пользователь <see cref="UserDto"/></returns>
        Task<BaseApiResponse<UserDto>> GetByFilter(BaseFilter filter);

        /// <summary>
        /// Получение пользователей по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Коллекция пользователей</returns>
        Task<IReadOnlyCollection<UserDto>> GetAll(BaseFilter filter = null);
    }
}
