namespace GastosResidenciais.Domain.Repositories;

public interface ISaveChangesUnit
{
    public Task Commit();
}
