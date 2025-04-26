namespace RenterScoreAPIv2.AutoMapper;

using global::AutoMapper;
using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.PropertyDetails;
using RenterScoreAPIv2.UserProfile;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<DateTime, string>()
            .ConvertUsing(src => src.ToString("yyyy-MM-ddTHH:mm:ss"));

        CreateMap<PropertyDetails, PropertyDetailsViewModel>()
            .IncludeMembers(src => src.Property)
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserProfile))
            .ForPath(dest => dest.User.PropertyRole, opt => opt.MapFrom(src => src.User.PropertyRole));

        CreateMap<Property, PropertyDetailsViewModel>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.HeroImage, opt => opt.MapFrom(src => $"http://renterscore.live/{src.HeroImage}"));
        CreateMap<Property, AddressViewModel>();
        
        CreateMap<UserProfile, UserProfileViewModel>()
            .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => $"http://renterscore.live/{src.ProfileImage}"));
    }
}