using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;

namespace GastosResidenciais.Application.UseCases.User.Get;

public interface IGetUserUseCase
{
    public Task<ResponseGetUserJson> Execute(RequestGetUserJson request);
}
