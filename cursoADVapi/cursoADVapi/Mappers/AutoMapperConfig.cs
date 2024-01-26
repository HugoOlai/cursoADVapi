using AutoMapper;

namespace cursoADVapi.Mappers
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<,>();
            });

            return config;
        }
    }
}
