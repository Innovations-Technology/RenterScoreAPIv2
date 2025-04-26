namespace RenterScoreAPIv2.AutoMapper;

using global::AutoMapper;
using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.UserProfile;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<DateTime, string>()
            .ConvertUsing(src => src.ToString("yyyy-MM-ddTHH:mm:ss"));

        CreateMap<PropertyWithUserProfile, PropertyViewModel>()
            .IncludeMembers(src => src.Property)
            .ForMember(src => src.User, opt => opt.MapFrom(src => src.UserProfile));

        CreateMap<Property, PropertyViewModel>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.HeroImage, opt => opt.MapFrom(src => $"http://renterscore.live/{src.HeroImage}"));
        CreateMap<Property, AddressViewModel>();
        
        CreateMap<UserProfile, UserProfileViewModel>()
            .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => $"http://renterscore.live/{src.ProfileImage}"));
        
    }
}