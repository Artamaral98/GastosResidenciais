namespace GastosResidenciais.Communication.Responses.UserResponses;

public class ResponseGetUserJson
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    public decimal TotalRevenue { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal Balance { get; set; }

    public List<TransactionResponse> Transactions { get; set; } = new List<TransactionResponse>();
}

public class TransactionResponse
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}
