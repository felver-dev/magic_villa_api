using AutoMapper;
using villaApi.DTOs;
using villaApi.Models;

namespace villaApi.Config
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			CreateMap<Villa, VillaCreateDTO>().ReverseMap();
			CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
		}
	}
}