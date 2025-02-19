using GastosResidenciais.Communication.Requests.UserRequests;
using GastosResidenciais.Communication.Responses.UserResponses;

namespace GastosResidenciais.Application.UseCases.User.Delete;

public interface IDeleteUserUseCase
{
    public Task<ResponseDeletedUserJson> Execute(RequestDeleteUserJson request);
}
