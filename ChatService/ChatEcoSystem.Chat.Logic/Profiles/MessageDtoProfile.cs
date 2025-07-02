using AutoMapper;
using ChatEcoSystem.Chat.Logic.Data;
using ChatEcoSystem.SharedLib.Contracts;
using System.Linq;
using System.Collections.Generic;

namespace ChatEcoSystem.Chat.Logic
{
    /// <summary>
    /// Профиль перевода из <see cref="MessageDto"/> в <see cref="Message"/> и обратно
    /// </summary>
    public class MessageDtoProfile : Profile
    {
        public MessageDtoProfile()
        {
            CreateMap<MessageDto, Message>()
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.SenderId))
                .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.ReceiverIds != null && src.ReceiverIds.Count > 0 ? src.ReceiverIds[0] : null))
                .ForMember(dest => dest.ChatRoom, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.SenderId))
                .ForMember(dest => dest.ReceiverIds, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.ReceiverId) ? new List<string> { src.ReceiverId } : new List<string>()))
                .ForMember(dest => dest.Metadata, opt => opt.Ignore());
        }
    }
}
