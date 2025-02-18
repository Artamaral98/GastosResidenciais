using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using GastosResidenciais.Infrastructure.DataAccess;
using FluentMigrator.Runner;
using GastosResidenciais.Infrastructure.Extensions;
using System.Reflection;
using GastosResidenciais.Infrastructure.DataAccess.Repositories;
using GastosResidenciais.Domain.Repositories;
using GastosResidenciais.Domain.Repositories.User;
using GastosResidenciais.Domain.Repositories.Transaction;

namespace GastosResidenciais.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        //Função que trará consigo todos os serviços de injeção de dependência da projeto de infraestrutura
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) //o This está relatando que a função é uma extensão de IServiceCollection
        {
            AddDbContext_SqlServer(services, configuration);
            AddFluentMigrator_SqlServer(services, configuration);
            AddRepositories(services);

        }

        private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

            services.AddDbContext<GastosResidenciaisDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddFluentMigrator_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("GastosResidenciais.Infrastructure")).For.All();
            });
        }

        //Injeção de dependência relacionado aos reopsitories
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISaveChangesUnit, SaveChangesUnit>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

        }

    }
}
