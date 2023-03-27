using AutoMapper;
using PaintingsWebApi.Dto;
using PaintingsWebApi.Models;
using System.Diagnostics.Metrics;

namespace PaintingsWebApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Painting, PaintingDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<GalleryDto, Gallery>();
            CreateMap<PaintingDto, Painting>();
            CreateMap<ReviewDto, Review>();
            CreateMap<ReviewerDto, Reviewer>();
            CreateMap<Gallery, GalleryDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
        }
    }
}
