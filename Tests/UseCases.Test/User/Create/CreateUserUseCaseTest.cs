using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using GastosResidenciais.Application.UseCases.User.Create;

namespace UseCases.Test.User.Create;

//classe responsável pelos useCases relacionados ao usuário
public class CreateUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestCreateUserJsonBuilder.Build();

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request); 

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);


    }

    //Função que constroi e injeta todas as dependencias para o useCase
    private CreateUserUseCase CreateUseCase(long? userId = null)
    {
        var userRepositoryBuilder = new UserRepositoryBuilder();
        var mapper = MapperBuilder.Build();
        var saveChangesUnit = SaveChangesUnitBuilder.Build();

        return new CreateUserUseCase(userRepositoryBuilder.Build(), mapper, saveChangesUnit);
    }

}
