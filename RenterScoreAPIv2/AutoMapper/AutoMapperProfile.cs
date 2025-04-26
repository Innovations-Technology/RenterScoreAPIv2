namespace RenterScoreAPIv2.AutoMapper;

using global::AutoMapper;
using RenterScoreAPIv2.Property;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Property, PropertyViewModel>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src));
        CreateMap<Property, AddressViewModel>();
        CreateMap<PropertyWithUserProfile, PropertyViewModel>()
            .IncludeMembers(s => s.Property);
    }
}