using GastosResidenciais.Domain.Enums;

namespace GastosResidenciais.Communication.Requests.TransactionRequests;

public class RequestCreateTransactionJson
{
    public string Description { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TransactionTypes? Types { get; set; } //0 para receita, 1 para despesa
    public long UserId { get; set; }
}
