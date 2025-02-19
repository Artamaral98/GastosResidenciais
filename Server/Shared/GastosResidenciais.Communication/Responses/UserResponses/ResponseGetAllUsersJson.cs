namespace GastosResidenciais.Communication.Responses.UserResponses;

public class ResponseGetAllUsersJson
{
    public List<UserSummaryResponse> Users { get; set; } = [];
    public GlobalTotalResponse GlobalTotal { get; set; } = new GlobalTotalResponse();
}

public class UserSummaryResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal Balance { get; set; }
}

public class GlobalTotalResponse
{
    public decimal TotalRevenue { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal Valor { get; set; }
}
