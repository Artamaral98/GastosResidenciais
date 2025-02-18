using GastosResidenciais.Communication.Responses;

namespace GastosResidenciais.Application.UseCases.Transactions.GetAll;

public interface IGetAllTransactionsUseCase
{
    public Task<List<ResponseGetAllTransactionsJson>> Execute();
}
