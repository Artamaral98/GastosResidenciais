using GastosResidenciais.Communication.Requests.TransactionRequests;
using GastosResidenciais.Communication.Responses.TransactionResponses;

namespace GastosResidenciais.Application.UseCases.Transactions.Get;

public interface IGetTransactionUseCase
{
    Task<List<ResponseGetTransactionJson>> Execute(RequestGetTransactionJson userId);
}
