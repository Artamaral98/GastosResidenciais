namespace GastosResidenciais.Communication.Responses.TransactionResponses;

public class ResponseGetTransactionJson
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Types { get; set; }
    public DateTime CreatedAt { get; set; }
}
