using AutoMapper;
using System.Linq;

namespace TVmaze.Domain.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cast, CastDTO>()
                .ForMember("id", src => src.MapFrom(x => x.person.id))
                .ForMember("name", src => src.MapFrom(x => x.person.name))
                .ForMember("birthday", src => src.MapFrom(x => x.person.birthday));
            CreateMap<Show, ShowDTO>().ForMember("cast", src => src.MapFrom(x => x._embedded.cast.OrderByDescending(y => y.person.birthday)));
        }
    }
}
