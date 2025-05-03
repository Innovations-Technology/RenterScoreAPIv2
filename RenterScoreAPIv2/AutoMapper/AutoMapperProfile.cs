namespace RenterScoreAPIv2.AutoMapper;

using global::AutoMapper;
using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.PropertyDetails;
using RenterScoreAPIv2.UserProfile;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.PropertyRating;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<DateTime, string>()
            .ConvertUsing(src => src.ToString("yyyy-MM-ddTHH:mm:ss"));

        CreateMap<PropertyDetailsWithImages, PropertyDetailsWithImagesViewModel>()
            .IncludeMembers(src => src.Property)
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserProfile))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.PropertyImages.Select(pi => $"https://renterscore.live/{pi.ImageUrl}")))
            .ForPath(dest => dest.User.PropertyRole, opt => opt.MapFrom(src => src.User.PropertyRole))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.PropertyRating));

        CreateMap<Property, PropertyDetailsWithImagesViewModel>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.HeroImage, opt => opt.MapFrom(src => $"https://renterscore.live/{src.HeroImage}"));
            
        CreateMap<Property, AddressViewModel>();
        
        CreateMap<UserProfile, UserProfileViewModel>()
            .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => $"https://renterscore.live/{src.ProfileImage}"));

        CreateMap<PropertyDetails, PropertyDetailsWithImages>();
        
        CreateMap<Rating, PropertyRatingViewModel>();
    }
}