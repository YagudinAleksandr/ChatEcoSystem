using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatEcoSystem.SharedLib.Abstractions;
using ChatEcoSystem.SharedLib.Contracts;
using AutoMapper;

namespace ChatEcoSystem.Auth.Logic
{
    internal class UserService : IUserService
    {
        #region CTOR
        /// <inheritdoc cref="IRepository{TEntity, TKey}"/>
        private readonly IRepository<User, Guid> _userRepository;

        /// <inheritdoc cref="IMapper"/>
        private readonly IMapper _mapper;

        public UserService(IRepository<User, Guid> repository, IMapper mapper)
        {
            _userRepository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<BaseApiResponse<UserDto>> Create(UserCreateDto userCreate)
        {
            var user = _mapper.Map<User>(userCreate);
            await _userRepository.Add(user);
            return new BaseApiResponse<UserDto> { StatusCode = 201, Body = _mapper.Map<UserDto>(user) };
        }

        public async Task<BaseApiResponse<bool>> Delete(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                return new BaseApiResponse<bool> { StatusCode = 404, Body = false };
            await _userRepository.Delete(user);
            return new BaseApiResponse<bool> { StatusCode = 200, Body = true };
        }

        public async Task<BaseApiResponse<bool>> ExistEmail(string email)
        {
            var users = await _userRepository.GetAll();
            var exists = users.Any(u => u.Email == email);
            return new BaseApiResponse<bool> { StatusCode = 200, Body = exists };
        }

        public async Task<IReadOnlyCollection<UserDto>> GetAll(BaseFilter filter = null)
        {
            var users = await _userRepository.GetAll(filter);
            return _mapper.Map<IReadOnlyCollection<UserDto>>(users.ToList());
        }

        public async Task<BaseApiResponse<UserDto>> GetByFilter(BaseFilter filter)
        {
            var users = await _userRepository.GetAll(filter);
            var user = users.FirstOrDefault();
            if (user == null)
                return new BaseApiResponse<UserDto> { StatusCode = 404, Body = null };
            return new BaseApiResponse<UserDto> { StatusCode = 200, Body = _mapper.Map<UserDto>(user) };
        }

        public async Task<BaseApiResponse<UserDto>> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                return new BaseApiResponse<UserDto> { StatusCode = 404, Body = null };
            return new BaseApiResponse<UserDto> { StatusCode = 200, Body = _mapper.Map<UserDto>(user) };
        }

        public async Task<BaseApiResponse<UserDto>> Update(UserUpdateDto userUpdate)
        {
            var user = await _userRepository.GetById(userUpdate.Id);
            if (user == null)
                return new BaseApiResponse<UserDto> { StatusCode = 404, Body = null };
            user.Name = userUpdate.Name;
            user.Email = userUpdate.Email;
            await _userRepository.Update(user);
            return new BaseApiResponse<UserDto> { StatusCode = 200, Body = _mapper.Map<UserDto>(user) };
        }

        public async Task<BaseApiResponse<UserDto>> UpdatePassword(UserUpdatePasswordDto userUpdatePassword)
        {
            var user = await _userRepository.GetById(userUpdatePassword.Id);
            if (user == null)
                return new BaseApiResponse<UserDto> { StatusCode = 404, Body = null };
            if (user.Password != userUpdatePassword.OldPassword)
                return new BaseApiResponse<UserDto> { StatusCode = 400, Body = null };
            if (userUpdatePassword.NewPassword != userUpdatePassword.ConfirmPassword)
                return new BaseApiResponse<UserDto> { StatusCode = 400, Body = null };
            user.Password = userUpdatePassword.NewPassword;
            await _userRepository.Update(user);
            return new BaseApiResponse<UserDto> { StatusCode = 200, Body = _mapper.Map<UserDto>(user) };
        }
    }
}
