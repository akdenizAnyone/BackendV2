using AutoMapper;
using Domain.Entities;
using System.Linq;
//announcement
using Application.Features.Users.Dtos;
using Application.Features.Users.ViewModels;

//posts
using Application.Features.Posts.Dtos;
using Application.Features.Follows.Dtos;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            //User
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<User,ProfileUserDto>().ReverseMap();
            CreateMap<ProfileUserDto,UserProfileVM>().ReverseMap();
            CreateMap<User,UserProfileVM>().ReverseMap();
            CreateMap<User,UserSearchDto>().ReverseMap();
            CreateMap<User,SuggestionUserDto>().ReverseMap();

            //Posts
            CreateMap<Post,PostDto>()          
                .ForMember(dto => dto.LikedByMe, opt => opt.Ignore())
                .ForMember(dto => dto.Likes, opt => opt.MapFrom(p => p.Likes.Count()))
                .ForMember(dto => dto.RePostedByMe, opt => opt.Ignore())
                .ForMember(dto => dto.RePosts, opt => opt.MapFrom(p => p.RePosts.Count()))
                .ForMember(dto => dto.Answers, opt => opt.MapFrom(p => p.Answers.Count()));

            CreateMap<Post,PostDto2>()          
                .ForMember(dto => dto.LikedByMe, opt => opt.Ignore())
                .ForMember(dto => dto.Likes, opt => opt.MapFrom(p => p.Likes.Count()))
                .ForMember(dto => dto.RePostedByMe, opt => opt.Ignore())
                .ForMember(dto => dto.RePosts, opt => opt.MapFrom(p => p.RePosts.Count()))
                .ForMember(dto => dto.Answers, opt => opt.MapFrom(p => p.Answers.Count()));




        }
    }
}
