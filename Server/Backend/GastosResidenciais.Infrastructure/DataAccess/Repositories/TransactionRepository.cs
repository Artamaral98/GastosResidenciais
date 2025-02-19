using GastosResidenciais.Domain.Entities;
using GastosResidenciais.Domain.Repositories.Transaction;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Infrastructure.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly GastosResidenciaisDbContext _dbContext;

        public TransactionRepository(GastosResidenciaisDbContext dbContext) => _dbContext = dbContext;

        //Método que trará todos os usuário em forma de lista, do mais recente para o menos recente.
        public async Task<IList<Transactions>> GetAllTransactions()
        {
            return await _dbContext.Transactions.Include(transaction =>transaction.User) //O Include faz o join com a tabela de usuários, possibilitando o retorno do nome e idade.
                .OrderByDescending(transaction => transaction.CreatedAt).ToListAsync();
        }

        //lista um usuário através do ID passado
        public async Task<IList<Transactions>> GetTransactionByUserId(long userId) => await _dbContext.Set<Transactions>()
            .Where(t => t.UserId == userId)
            .ToListAsync();

        public async Task AddTransaction(Transactions transaction) => await _dbContext.Transactions.AddAsync(transaction);

        public async Task DeleteTransaction(long id)
        {
            var transaction = await _dbContext.Transactions.FirstOrDefaultAsync(transaction => id == transaction.Id);
            if (transaction is null)
                return;

            _dbContext.Transactions.Remove(transaction);
        }

        public async Task UpdateTransaction(Transactions transaction)
        {
            var transactionExists = await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == transaction.Id);

            if (transactionExists != null)
            {
                transactionExists.Description = transaction.Description;
                transactionExists.Valor = transaction.Valor;
                transactionExists.Types = transaction.Types;
                transactionExists.UpdatedAt = DateTime.UtcNow;

            }

        }
    }


}

