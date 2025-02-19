using AutoMapper;
using GastosResidenciais.Communication.Responses.TransactionResponses;
using GastosResidenciais.Domain.Repositories.Transaction;

namespace GastosResidenciais.Application.UseCases.Transactions.GetAll;

class GetAllTransactionsUseCase : IGetAllTransactionsUseCase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public GetAllTransactionsUseCase(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task <List<ResponseGetAllTransactionsJson>> Execute()
    {
        var transactions = await _transactionRepository.GetAllTransactions();
        return _mapper.Map<List<ResponseGetAllTransactionsJson>>(transactions);

    }
}
