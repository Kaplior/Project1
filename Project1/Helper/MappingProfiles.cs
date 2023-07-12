using AutoMapper;
using Project1.Dto;
using Project1.Models;

namespace Project1.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<TrainDriver, TrainDriverDto>();

        }
    }
}
