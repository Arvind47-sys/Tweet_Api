using AutoMapper;
using Tweet_Api.DTOs;
using Tweet_Api.Entities;

namespace Tweet_Api.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TweetDto, Tweet>()
              .ForMember(dest => dest.LikedBy, opt => opt.Ignore())
              .ForMember(dest => dest.Replies, opt => opt.Ignore())
              .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.UserId))
              .ForMember(dest => dest.TweetId, opt => opt.MapFrom(src => src.TweetId))
              .ReverseMap();
            CreateMap<AppUser, RegisterDto>().ReverseMap();
            CreateMap<Replies, ReplyDto>().ReverseMap();
            CreateMap<Likes, LikeDto>().ReverseMap();
            CreateMap<AppUser, UserDetailDto>().ReverseMap();
        }
    }
}