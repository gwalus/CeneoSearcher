using AutoMapper;
using Shared.Dtos;
using Shared.Model;

namespace WebEngine.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
