using AutoMapper;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;
using GastosResidenciais.Domain.Repositories;
using GastosResidenciais.Domain.Repositories.Transaction;
using GastosResidenciais.Domain.Repositories.User;
using GastosResidenciais.Exceptions;

namespace GastosResidenciais.Application.UseCases.Transactions.Create;

public class CreateTransactionUseCase : ICreateTransactionUseCase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private readonly ISaveChangesUnit _saveChangesUnit;
    private readonly IUserRepository _userRepository;

    public CreateTransactionUseCase(ITransactionRepository transactionRepository, IMapper mapper, ISaveChangesUnit saveChangesUnit, IUserRepository userRepository)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _saveChangesUnit = saveChangesUnit;
        _userRepository = userRepository;
    }

    public async Task<ResponseCreateTransactionJson> Execute(RequestCreateTransactionJson request)
    {
        await Validate(request);

        // Mapeando a request, evitando mapeamento manual. 
        var transaction = _mapper.Map<Domain.Entities.Transactions>(request);

        // Criando nova transação para usuários
        await _transactionRepository.AddTransaction(transaction);

        //Salvando as alterações no DB
        await _saveChangesUnit.Commit();

        // Retornando o response com o ID gerado
        return _mapper.Map<ResponseCreateTransactionJson>(transaction);
    }

    private async Task Validate(RequestCreateTransactionJson request)
    {
        var validator = new CreateTransactionValidator(_userRepository);
        var result = await validator.ValidateAsync(request);

        if (result.IsValid == false) //Retornará true caso a requisição seja válida ou false caso alguma informação esteja inválida.
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList(); //Buscando os erros contidos nos erros da validação e convertendo para o tipo List
            throw new ErrorOnValidationException(errorMessages);

        }

    }
}
