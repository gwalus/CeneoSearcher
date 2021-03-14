using AutoMapper;
using Shared.Dtos;
using WebEngine.Model;

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
