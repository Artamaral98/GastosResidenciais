using GastosResidenciais.Communication.Responses.UserResponses;

namespace GastosResidenciais.Application.UseCases.User.GetAll;

public interface IGetAllUsersUseCase
{
    Task<ResponseGetAllUsersJson> Execute();
}
