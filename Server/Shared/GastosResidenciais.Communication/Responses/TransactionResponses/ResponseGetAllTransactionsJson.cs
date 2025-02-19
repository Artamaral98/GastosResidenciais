namespace GastosResidenciais.Communication.Responses.TransactionResponses;

public class ResponseGetAllTransactionsJson
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Types { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    //Informações do usuário
    public long UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}
