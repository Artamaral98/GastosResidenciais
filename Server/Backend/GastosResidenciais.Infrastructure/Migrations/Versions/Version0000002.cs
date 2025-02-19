using FluentMigrator;

namespace GastosResidenciais.Infrastructure.Migrations.Versions;

[Migration(2, "Criação da tabela com as informações das transações")]
public class Version0000002 : VersionBase
{
    public override void Up()
    {
        CreateTable("Transactions")
            .WithColumn("Description").AsString(22).NotNullable()
            .WithColumn("Valor").AsDecimal().NotNullable()
            .WithColumn("Types").AsInt32().NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("UpdatedAt").AsDateTime().Nullable()

            //Uma pessoa tem N transações, desta forma, deve ser colocado em "Transações" o Id desta pessoa como uma chave estrangeira para relaciona-los e facilitar consultas SQL.
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Transactions_User_Id", "Users", "Id")
            .OnDelete(System.Data.Rule.Cascade); //Ao deletar um usuário, suas transações também serão deletadas
    }
}
