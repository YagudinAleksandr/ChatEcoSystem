using AutoMapper;
using ChatEcoSystem.SharedLib.Contracts;

namespace ChatEcoSystem.Auth.Logic.Profiles
{
    /// <summary>
    /// Профиль из <see cref="UserCreateDto"/> в <see cref="User"/>
    /// </summary>
    internal class UserCreateDtoProfile : Profile
    {
        public UserCreateDtoProfile() 
        {
            CreateMap<UserCreateDto, User>();
        }
    }
}
