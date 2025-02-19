using AutoMapper;
using GastosResidenciais.Communication.Requests.TransactionRequests;
using GastosResidenciais.Communication.Responses.TransactionResponses;
using GastosResidenciais.Domain.Repositories;
using GastosResidenciais.Domain.Repositories.Transaction;
using GastosResidenciais.Domain.Repositories.User;
using GastosResidenciais.Exceptions;

namespace GastosResidenciais.Application.UseCases.Transactions.Update;

public class UpdateTransactionUseCase : IUpdateTransactionUseCase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private readonly ISaveChangesUnit _saveChangesUnit;
    private readonly IUserRepository _userRepository;

    public UpdateTransactionUseCase(ITransactionRepository transactionRepository, IMapper mapper, ISaveChangesUnit saveChangesUnit, IUserRepository userRepository)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _saveChangesUnit = saveChangesUnit;
        _userRepository = userRepository;
    }

    public async Task<ResponseUpdatedTransactionJson> Execute(RequestUpdateTransactionJson request)
    {
        Validate(request);

        var isUnderage = await _userRepository.IsUnderage(request.UserId);

        var transaction = _mapper.Map<Domain.Entities.Transactions>(request);

        if (isUnderage)
        {
            if (request.Types == Domain.Enums.TransactionTypes.revenue)
            {
                throw new ErrorOnValidationException(new List<string> { ResourceMessagesException.INVALID_TRANSACTION_FOR_UNDERAGE });
            }

        }

        await _transactionRepository.UpdateTransaction(transaction);

        await _saveChangesUnit.Commit();

        return _mapper.Map<ResponseUpdatedTransactionJson>(transaction);

    }

    private void Validate(RequestUpdateTransactionJson request)
    {
        var validator = new UpdateTransactionValidator(_userRepository);
        var result = validator.Validate(request);

        if (result.IsValid == false) //Retornará true caso a requisição seja válida ou false caso alguma informação esteja inválida.
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList(); //Buscando os erros contidos nos erros da validação e convertendo para o tipo List
            throw new ErrorOnValidationException(errorMessages);

        }

    }
}

