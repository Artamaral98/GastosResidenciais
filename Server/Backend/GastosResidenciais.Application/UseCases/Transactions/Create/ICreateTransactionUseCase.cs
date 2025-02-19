using GastosResidenciais.Communication.Requests.TransactionRequests;
using GastosResidenciais.Communication.Responses.TransactionResponses;

namespace GastosResidenciais.Application.UseCases.Transactions.Create;

public interface ICreateTransactionUseCase
{
    public Task<ResponseCreateTransactionJson> Execute(RequestCreateTransactionJson request);
}
