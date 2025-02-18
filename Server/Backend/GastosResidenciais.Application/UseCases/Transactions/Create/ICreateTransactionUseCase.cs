using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;

namespace GastosResidenciais.Application.UseCases.Transactions.Create;

public interface ICreateTransactionUseCase
{
    public Task<ResponseCreateTransactionJson> Execute(RequestCreateTransactionJson request);
}
