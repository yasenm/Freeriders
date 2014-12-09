namespace FreeRiders.Web.ViewModels.Review
{
    using AutoMapper;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class ReviewCollectionViewModel : IMapFrom<Review>, IHaveCustomMappings
    {
        [UIHint("DescriptionFullText")]
        public string Content { get; set; }

        [UIHint("UserBasicViewModelMediumLink")]
        public string AuthorName { get; set; }

        [UIHint("StarsDisplay")]
        public double Stars { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Review, ReviewCollectionViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(r => r.Author.UserName));
        }
    }
}