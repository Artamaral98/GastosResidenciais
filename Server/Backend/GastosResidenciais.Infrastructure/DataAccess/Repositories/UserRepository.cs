using GastosResidenciais.Domain.Entities;
using GastosResidenciais.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GastosResidenciaisDbContext _dbContext;

        //Passando o banco de dados para o construtor do repositório

        public UserRepository(GastosResidenciaisDbContext dbContext) => _dbContext = dbContext;

        //Função responsável por listar todos os usuários e as suas transações
        public async Task<IList<User>> GetAllUsersWithTransactions()
        {
            //o include faz a inclusão das informações da tabela de transações, que está relacionada com usuários e retorna uma lista com ToListAsync .
            return await _dbContext.Users.Include(t => t.Transactions).ToListAsync();

        }

        //Função responsável por verificar se existe um usuário com o ID fornecido
        public async Task<bool> Exists(long userId) => await _dbContext.Users.AnyAsync(u => u.Id == userId);


        //Função responsável por listar um usuário específico
        public async Task<User> GetUserById(long userId)
        {
            var user = await _dbContext
                .Set<User>()
                .AsNoTracking() //Garante que não haja alteração neste usuário
                .Include(t => t.Transactions)
                .FirstOrDefaultAsync(user => user.Id == userId);

            return user;
        }

        //Função responsável por adicionar um novo usuário
        public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

        //Função responsável por deletar um usuário, e suas respectivas transações
        public async Task Delete(long userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => userId == user.Id);
            if (user is null)
                return;

            var transactions = _dbContext.Transactions.Where(t => t.UserId == user.Id);

            _dbContext.Transactions.RemoveRange(transactions); //garantir que todas as transações sejam removidas antes de remover o usuário.

            _dbContext.Users.Remove(user!);
        }

        //Método que verifica a idade do usuário
        public async Task<bool> IsUnderage(long userId)
        {
            var user = await _dbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user != null && user.Age < 18;
        }
    }
}
