using AutoMapper;
using GastosResidenciais.Application.UseCases.User.Get;
using GastosResidenciais.Communication.Requests.TransactionRequests;
using GastosResidenciais.Communication.Responses.TransactionResponses;
using GastosResidenciais.Domain.Repositories.Transaction;
using GastosResidenciais.Domain.Repositories.User;
using GastosResidenciais.Exceptions;

namespace GastosResidenciais.Application.UseCases.Transactions.Get;

public class GetTransactionsUseCase : IGetTransactionUseCase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetTransactionsUseCase(ITransactionRepository transactionRepository, IMapper mapper, IUserRepository userRepository)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<List<ResponseGetTransactionJson>> Execute(RequestGetTransactionJson request)
    {
        await Validate(request);

        var transactions = await _transactionRepository.GetTransactionByUserId(request.UserId);

        return _mapper.Map<List<ResponseGetTransactionJson>>(transactions);
    }

    private async Task Validate(RequestGetTransactionJson request)
    {
        var validator = new GetTransactionValidator(_userRepository);
        var result = await validator.ValidateAsync(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);

        }

    }
}
