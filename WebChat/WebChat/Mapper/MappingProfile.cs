using AutoMapper;
using WebChat.DTOs;
using WebChat.Models;

namespace WebChat.Mapper;

public class MappingProfile:Profile
{
    public MappingProfile() 
    {
        CreateMap<ChatMessage, MessageDto>()
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.Username))
            .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.Receiver.Username));

        CreateMap<User, UserDto>();
    }
}
