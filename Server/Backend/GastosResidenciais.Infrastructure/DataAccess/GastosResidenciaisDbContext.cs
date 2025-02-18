using GastosResidenciais.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Infrastructure.DataAccess;

public class GastosResidenciaisDbContext : DbContext
{
    public GastosResidenciaisDbContext(DbContextOptions options) : base(options) { }

    //Comunicando ao EntityFramework que existe as tabelas tipo User e Transactions.
    public DbSet<User> Users { get; set; }

    public DbSet<Transactions> Transactions { get; set; }

    //Comunicando que será utilizado as configurações do projeto presentes no assembly
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GastosResidenciaisDbContext).Assembly);
    }
}
