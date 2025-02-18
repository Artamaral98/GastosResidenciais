using GastosResidenciais.Domain.Repositories.Transaction;
using Moq;

namespace CommonTestUtilities.Repositories;

public class TransactionRepositoryBuilder
{
    private readonly Mock<ITransactionRepository> _transactionRepository;

    public TransactionRepositoryBuilder()
    {
        _transactionRepository = new Mock<ITransactionRepository>();
    }
}
