using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;
using GastosResidenciais.Domain.Repositories;
using GastosResidenciais.Domain.Repositories.Transaction;
using GastosResidenciais.Exceptions;

namespace GastosResidenciais.Application.UseCases.Transactions.Delete;

public class DeleteTransactionUseCase : IDeleteTransactionUseCase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ISaveChangesUnit _saveChangesUnit;

    public DeleteTransactionUseCase(ITransactionRepository transactionRepository, ISaveChangesUnit saveChangesUnit)
    {
        _transactionRepository = transactionRepository;
        _saveChangesUnit = saveChangesUnit;

    }

    public async Task<ResponseDeletedTransactionJson> Execute(RequestDeleteTransactionJson request)
    {
        //Chamando o método de validação para verificar se o Id da transação foi passado na request
        Validate(request);

        var transactionId = request.Id;

        await _transactionRepository.DeleteTransaction(transactionId);
        await _saveChangesUnit.Commit();

        return new ResponseDeletedTransactionJson
        {
            Message = "Transação deletada"
        };
    }

    private void Validate(RequestDeleteTransactionJson request)
    {
        var validator = new DeleteTransactionValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);

        }

    }
}
