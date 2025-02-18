using AutoMapper;
using GastosResidenciais.Application.UseCases.User.GetAll;
using GastosResidenciais.Communication.Responses;
using GastosResidenciais.Domain.Enums;
using GastosResidenciais.Domain.Repositories.User;

namespace GastosResidenciais.Application.UseCases.User.Get;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllUsersUseCase(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ResponseGetAllUsersJson> Execute()
    {
        // Obtém todos os usuários com suas respectivas transações
        var users = await _userRepository.GetAllUsersWithTransactions();

        // Instancia o objeto de resposta
        var response = new ResponseGetAllUsersJson();

        // Realiza o cálculo de receitas, despesas e saldo e modifica a response adicionando os resultados
        BalanceCalculate(users, response);

        return response;
    }

    // Método responsável por calcular receitas, despesas e saldo de cada usuário
    private void BalanceCalculate(IEnumerable<Domain.Entities.User> users, ResponseGetAllUsersJson response)
    {
        decimal totalRevenues = 0;
        decimal totalExpenses = 0;
        decimal globalTotal = 0;

        // Itera sobre todos os usuários para calcular receitas, despesas e saldo
        foreach (var user in users)
        {
            var revenues = user.Transactions
                               .Where(t => t.Types == TransactionTypes.revenue)
                               .Sum(t => t.Valor);

            var expenses = user.Transactions
                               .Where(t => t.Types == TransactionTypes.expense)
                               .Sum(t => t.Valor);

            var balance = revenues - expenses;

            totalRevenues += revenues;
            totalExpenses += expenses;
            globalTotal += balance;

            // Mapeia automaticamente para UserSummaryResponse usando AutoMapper
            var userSummary = _mapper.Map<UserSummaryResponse>(user);
            userSummary.TotalRevenue = revenues;
            userSummary.TotalExpenses = expenses;
            userSummary.Balance = balance;

            response.Users.Add(userSummary);
        }

        // Adiciona o total geral na response
        response.GlobalTotal = new GlobalTotalResponse
        {
            TotalRevenue = totalRevenues,
            TotalExpenses = totalExpenses,
            Valor = globalTotal
        };
    }
}
