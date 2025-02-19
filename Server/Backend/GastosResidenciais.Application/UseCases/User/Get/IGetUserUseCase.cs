using GastosResidenciais.Communication.Requests.UserRequests;
using GastosResidenciais.Communication.Responses.UserResponses;

namespace GastosResidenciais.Application.UseCases.User.Get;

public interface IGetUserUseCase
{
    public Task<ResponseGetUserJson> Execute(RequestGetUserJson request);
}
