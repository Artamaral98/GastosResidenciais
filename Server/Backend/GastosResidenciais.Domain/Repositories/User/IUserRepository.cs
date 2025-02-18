namespace GastosResidenciais.Domain.Repositories.User;


//Interface que dita os contratos referente aos métodos do repositório de usuários.
public interface IUserRepository
{
    public Task<IList<Entities.User>> GetAllUsersWithTransactions();

    public Task<bool> Exists(long userId);

    public Task<Entities.User> GetUserById(long userId);
    public Task Add(Entities.User user);

    public Task Delete(long userId);

    public Task<bool> IsUnderage(long userId);
}
