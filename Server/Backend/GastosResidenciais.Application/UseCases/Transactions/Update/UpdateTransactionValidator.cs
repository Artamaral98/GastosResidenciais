using System.Transactions;
using FluentValidation;
using GastosResidenciais.Communication.Requests.TransactionRequests;
using GastosResidenciais.Domain.Repositories.User;
using GastosResidenciais.Exceptions;

namespace GastosResidenciais.Application.UseCases.Transactions.Update;

public class UpdateTransactionValidator : AbstractValidator<RequestUpdateTransactionJson>
{
    private readonly IUserRepository _repository;
    public UpdateTransactionValidator(IUserRepository repository)
    {
        _repository = repository;

        RuleFor(transaction => transaction.Description).NotEmpty().WithMessage(ResourceMessagesException.DESCRIPTION_EMPTY);
        RuleFor(transaction => transaction.Description).MaximumLength(25).WithMessage(ResourceMessagesException.DESCRIPTION_MAX_LENGTH);
        RuleFor(transaction => transaction.Valor).NotEmpty().WithMessage(ResourceMessagesException.VALOR_EMPTY);
        RuleFor(transaction => transaction.Valor).GreaterThan(0).WithMessage(ResourceMessagesException.VALOR_GREATER_THAN_0);
        RuleFor(transaction => transaction.Valor).LessThan(999999999999).WithMessage(ResourceMessagesException.VALOR_LESSER_THAN);
        RuleFor(transaction => transaction.Types).NotEmpty().WithMessage(ResourceMessagesException.TYPES_EMPTY);
        RuleFor(transaction => transaction.Types).IsInEnum().WithMessage(ResourceMessagesException.TYPE_INVALID);
        RuleFor(transaction => transaction.Id).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);

        ////Verificar se o usuário possui mais de 18 anos. Caso tenha sido enviado uma receita e o usuário seja menor de 18, será retornado uma mensagem de erro.
        //RuleFor(transaction => transaction).MustAsync(async (transaction, _) =>
        //{
        //    return !(await _repository!.IsUnderage(transaction.UserId) && transaction.Types == Domain.Enums.TransactionTypes.revenue);
        //})
        //    .WithMessage(ResourceMessagesException.INVALID_TRANSACTION_FOR_UNDERAGE);
    }

}
