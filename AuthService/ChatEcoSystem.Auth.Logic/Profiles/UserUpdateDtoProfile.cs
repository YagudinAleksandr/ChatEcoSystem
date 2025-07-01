using AutoMapper;
using ChatEcoSystem.SharedLib.Contracts;

namespace ChatEcoSystem.Auth.Logic.Profiles
{
    /// <summary>
    /// Профиль <see cref="UserUpdateDto"/> в <see cref="User"/> и <see cref="UserUpdatePasswordDto"/> в <see cref="User"/>
    /// </summary>
    internal class UserUpdateDtoProfile : Profile
    {
        public UserUpdateDtoProfile() 
        {
            CreateMap<UserUpdateDto, User>();
            CreateMap<UserUpdatePasswordDto, User>();
        }
    }
}
