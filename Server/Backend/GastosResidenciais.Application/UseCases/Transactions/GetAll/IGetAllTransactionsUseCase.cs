using GastosResidenciais.Communication.Responses.TransactionResponses;

namespace GastosResidenciais.Application.UseCases.Transactions.GetAll;

public interface IGetAllTransactionsUseCase
{
    public Task<List<ResponseGetAllTransactionsJson>> Execute();
}
