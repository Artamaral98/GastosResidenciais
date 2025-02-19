using GastosResidenciais.Domain.Enums;

namespace GastosResidenciais.Communication.Requests.TransactionRequests;

public class RequestUpdateTransactionJson
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TransactionTypes? Types { get; set; }
}
