using GastosResidenciais.Domain.Enums;

namespace GastosResidenciais.Communication.Responses.TransactionResponses;

public class ResponseUpdatedTransactionJson
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Types { get; set; } = string.Empty;
}
