namespace GastosResidenciais.Infrastructure.Migrations.Versions
{
    public class Version0000003 : VersionBase
    {
        public override void Up()
        {
            Alter.Table("Transactions")
                .AlterColumn("Valor").AsDecimal(18, 2).NotNullable();
        }
    }
}
