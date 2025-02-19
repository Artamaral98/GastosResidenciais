using GastosResidenciais.Communication.Requests.TransactionRequests;
using GastosResidenciais.Communication.Responses.TransactionResponses;

namespace GastosResidenciais.Application.UseCases.Transactions.Update;

public interface IUpdateTransactionUseCase
{
    public Task<ResponseUpdatedTransactionJson> Execute(RequestUpdateTransactionJson request);
}
