using GastosResidenciais.Domain.Entities;

namespace GastosResidenciais.Domain.Repositories.Transaction;

public interface ITransactionRepository
{
    public Task<IList<Transactions>> GetAllTransactions();

    public Task<IList<Transactions>> GetTransactionByUserId(long userId);
    public Task AddTransaction(Transactions transaction);

    public Task DeleteTransaction(long id);

    public Task UpdateTransaction(Transactions transaction);

}
