namespace GastosResidenciais.Communication.Responses;

public class ResponseCreateTransactionJson
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Types { get; set; } = string.Empty;
}
