using FluentMigrator;
//O fluentmigrator reduz o número de arquivos criados no momento da migração para o banco de dados. Além disso, possui uma sintaxe mais clara para criação de tabelas.

namespace GastosResidenciais.Infrastructure.Migrations.Versions;

//número da versão, descrição
[Migration(1, "Criação da tabela com as informações do usuário")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        CreateTable("Users")
            .WithColumn("Name").AsString(30).NotNullable()
            .WithColumn("Age").AsInt32().NotNullable();
    }
}
