using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;

namespace GastosResidenciais.Application.UseCases.Transactions.Get;

public interface IGetTransactionUseCase
{
    Task<List<ResponseGetTransactionJson>> Execute(RequestGetTransactionJson userId);
}
