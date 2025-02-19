namespace GastosResidenciais.Application.UseCases.Transactions.Delete;
using FluentValidation;
using GastosResidenciais.Communication.Requests.TransactionRequests;
using GastosResidenciais.Exceptions;

class DeleteTransactionValidator : AbstractValidator<RequestDeleteTransactionJson>
{
    public DeleteTransactionValidator()
    {
        RuleFor(transaction => transaction.Id).NotEmpty().WithMessage(ResourceMessagesException.ID_EMPTY);
    }
}
