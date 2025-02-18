using AutoMapper;
using GastosResidenciais.Application.Services.AutoMapper;

namespace CommonTestUtilities.Mapper;


//Auto mapper builder para os casos de teste
public class MapperBuilder
{
    public static IMapper Build()
    {
        return new MapperConfiguration(options =>
        {
            options.AddProfile(new Automapping());
        }).CreateMapper();
    }
}
