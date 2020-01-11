using AutoMapper;
using Web.Extensions;
using Web.Models;

namespace Web.ViewModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostViewModel>();

            CreateMap<Post, PostListViewModel>()
                .ForMember(dest => dest.PostCategoryDisplay,
                    opt => opt.MapFrom(src => src.PostCategory.Name))
                .ForMember(dest => dest.CreatedTime,
                    opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.PostTypeDisplay,
                    opt => opt.MapFrom(src => src.PostType.ToPostTypeDisplayName()))
                .ReverseMap();
            ;
        }
    }
}