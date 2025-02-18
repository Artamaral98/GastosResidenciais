using GastosResidenciais.Communication.Responses;

namespace GastosResidenciais.Application.UseCases.User.GetAll;

public interface IGetAllUsersUseCase
{
    Task<ResponseGetAllUsersJson> Execute();
}
