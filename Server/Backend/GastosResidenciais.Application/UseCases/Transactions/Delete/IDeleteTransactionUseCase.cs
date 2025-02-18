using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;

namespace GastosResidenciais.Application.UseCases.Transactions.Delete;

public interface IDeleteTransactionUseCase
{
    public Task<ResponseDeletedTransactionJson> Execute(RequestDeleteTransactionJson request);
}
