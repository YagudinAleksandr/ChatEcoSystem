using AutoMapper;
using ChatEcoSystem.SharedLib.Contracts;

namespace ChatEcoSystem.Auth.Logic.Profiles
{
    /// <summary>
    /// Профили из <see cref="User"/> в <see cref="UserDto"/> и обратно
    /// </summary>
    internal class UserDtoProfile : Profile
    {
        public UserDtoProfile() 
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
