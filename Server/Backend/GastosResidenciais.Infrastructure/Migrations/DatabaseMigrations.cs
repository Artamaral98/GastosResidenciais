using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace GastosResidenciais.Infrastructure.Migrations
{
    //Responsável por criar o DB através da API e realizar as migrações
    public static class DataBaseMigrations
    {
        public static void Migrate(string connectionString, IServiceProvider serviceProvider)
        {
            EnsureDataBaseCreated(connectionString);
            MigrationDatabase(serviceProvider);
        }

        //Verifica se o DB já está criado, se não estiver, ele cria com o nome dado na ConnectionString
        private static void EnsureDataBaseCreated(string connectionString)
        {
            //recuperar o nome do DB da connectionString através de um StringBuilder, que é um objeto contendo todos os atributos do DB;
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = connectionStringBuilder.InitialCatalog; //puxa o nome do DB que será criado

            connectionStringBuilder.Remove("Database");

            var parameters = new DynamicParameters();
            parameters.Add("name", databaseName);

            using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

            //Irá retornar uma lista de schemas que tenham um nome específico, no caso o nome do DB, através de uma verificação para informar se há alguma tabela com o nome dado no parameters
            var records = dbConnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

            if (records.Any() == false) //Caso não exista nenhum schema com esse nome, estando o records vazio, ele criará o schema
            {
                dbConnection.Execute($"CREATE DATABASE {databaseName}");
            }
        }

        //Essa função de fato faz com que todas as alterações sejam enviadas para o DB
        private static void MigrationDatabase(IServiceProvider serviceProvider) //Serviço da injeção de dependência
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>(); //serviço configurado em AddFluentMigrator_SqlServer
            runner.ListMigrations(); //Esse código lista todas as migrações
            runner.MigrateUp(); //realiza a comunicação e executa a migração
        }

    }
}



