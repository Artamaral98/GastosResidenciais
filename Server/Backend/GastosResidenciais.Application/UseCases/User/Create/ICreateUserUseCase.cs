using GastosResidenciais.Communication.Requests.UserRequests;
using GastosResidenciais.Communication.Responses.UserResponses;

namespace GastosResidenciais.Application.UseCases.User.Create;

//Interface que integra os métodos contidos no use cases. Esta interface tem o objetivo da utilização da injeção de dependências, facilitando a execução do método Execute no userController.
public interface ICreateUserUseCase
{
    public Task<ResponseCreatedUserJson> Execute(RequestCreateUserJson request);
}
