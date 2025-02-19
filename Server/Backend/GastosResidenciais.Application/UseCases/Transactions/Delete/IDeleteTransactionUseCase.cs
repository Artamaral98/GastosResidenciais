using GastosResidenciais.Communication.Requests.TransactionRequests;
using GastosResidenciais.Communication.Responses.TransactionResponses;

namespace GastosResidenciais.Application.UseCases.Transactions.Delete;

public interface IDeleteTransactionUseCase
{
    public Task<ResponseDeletedTransactionJson> Execute(RequestDeleteTransactionJson request);
}
