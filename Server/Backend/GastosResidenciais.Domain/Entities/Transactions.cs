using System.ComponentModel.DataAnnotations.Schema;
using GastosResidenciais.Domain.Enums;

namespace GastosResidenciais.Domain.Entities
{
    public class Transactions : EntityBase
    {
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
        public TransactionTypes Types { get; set; } //0 para receitas, 1 para despesas
        public long UserId { get; set; }

        //Será usado para ordenar as transaçoes
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        //Permite o relacionamento e a consulta para buscar dados do usuário.
        public User User { get; set; } = null!;

    }
}
