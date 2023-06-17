using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOS;

namespace NZWalks.API.Mappings
{
    public class Mapping : Profile
    {
        public Mapping() 
        { 
            CreateMap<Region, RegionsDTO>().ReverseMap();
        }

      
    }
}
