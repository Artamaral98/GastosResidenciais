using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;

namespace GastosResidenciais.Application.UseCases.User.Delete;

public interface IDeleteUserUseCase
{
    public Task<ResponseDeletedUserJson> Execute(RequestDeleteUserJson request);
}
