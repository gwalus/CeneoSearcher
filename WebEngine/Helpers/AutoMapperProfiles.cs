using AutoMapper;
using Shared.Dtos;
using Shared.Model;

namespace WebEngine.Helpers
{
    /// <summary>
    /// Class contains profiles to automapping between classes.
    /// </summary>
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
