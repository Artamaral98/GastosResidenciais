using GastosResidenciais.Domain.Repositories;

namespace GastosResidenciais.Infrastructure.DataAccess;

//A regra de negócios, localizada em application, deverá receber esta classe como injeção de dependencia, Logo, receberá através de uma interface localizada em Domain
//Retorna um funççao para salvar as alterações no banco de dados, evitando repetição de código.
public class SaveChangesUnit : ISaveChangesUnit
{
    private readonly GastosResidenciaisDbContext _dbContext;

    public SaveChangesUnit(GastosResidenciaisDbContext dbContext) => _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();

}
