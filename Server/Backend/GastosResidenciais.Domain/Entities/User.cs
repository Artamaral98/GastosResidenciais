namespace GastosResidenciais.Domain.Entities
{
   public class User : EntityBase
    {
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; } 

        public ICollection<Transactions> Transactions { get; set; } = [];
    }
}
