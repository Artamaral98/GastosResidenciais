using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace GastosResidenciais.Infrastructure.Migrations.Versions;

//Essa classe será responsável por sempre criar a tabela ID, comum às entidades.
public abstract class VersionBase : ForwardOnlyMigration
{
    protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
    {
        return Create.Table(table)
            .WithColumn("Id").AsInt64().PrimaryKey().Identity();

    }
}

