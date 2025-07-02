using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ChatEcoSystem.Chat.Logic.Data.Entities;
using ChatEcoSystem.SharedLib.Contracts;

namespace ChatEcoSystem.Chat.Logic.Profiles
{
    /// <summary>
    /// Профиль из <see cref="ChatRoom"/> в <see cref="ChatRoomDto"/> и обратно
    /// </summary>
    internal class ChatRoomProfile : Profile
    {
        public ChatRoomProfile()
        {
            CreateMap<ChatRoom, ChatRoomDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type))
                .ForMember(dest => dest.MemberIds, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.MemberIdsSerialized)
                        ? new List<string>()
                        : src.MemberIdsSerialized.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.Metadata, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (ChatRoomTypeEnum)src.Type))
                .ForMember(dest => dest.MemberIdsSerialized, opt => opt.MapFrom(src => src.MemberIds != null ? string.Join(",", src.MemberIds) : string.Empty))
                .ForMember(dest => dest.Messages, opt => opt.Ignore());
        }
    }
}
