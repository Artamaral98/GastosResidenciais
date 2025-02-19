using FluentValidation;
using GastosResidenciais.Communication.Requests.TransactionRequests;
using GastosResidenciais.Domain.Repositories.User;
using GastosResidenciais.Exceptions;

namespace GastosResidenciais.Application.UseCases.Transactions.Create;

public class CreateTransactionValidator : AbstractValidator<RequestCreateTransactionJson>
{
    private readonly IUserRepository _repository;
    public CreateTransactionValidator(IUserRepository repository)
    {
        _repository = repository;

        RuleFor(transaction => transaction.Description).NotEmpty().WithMessage(ResourceMessagesException.DESCRIPTION_EMPTY);
        RuleFor(transaction => transaction.Description).Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage(ResourceMessagesException.DESCRIPTION_SPECIAL_CARACTERS);
        RuleFor(transaction => transaction.Description).MaximumLength(25).WithMessage(ResourceMessagesException.DESCRIPTION_MAX_LENGTH);
        RuleFor(transaction => transaction.Valor).NotEmpty().WithMessage(ResourceMessagesException.VALOR_EMPTY);
        RuleFor(transaction => transaction.Valor).GreaterThan(0).WithMessage(ResourceMessagesException.VALOR_GREATER_THAN_0);
        RuleFor(transaction => transaction.Valor).LessThan(999999999999).WithMessage(ResourceMessagesException.VALOR_LESSER_THAN);
        RuleFor(transaction => transaction.Types).NotEmpty().WithMessage(ResourceMessagesException.TYPES_EMPTY);
        RuleFor(transaction => transaction.Types).IsInEnum().WithMessage(ResourceMessagesException.TYPE_INVALID);
        RuleFor(transaction => transaction.UserId).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);

        //Verificar se existe usuário correspondente ao Id fornecido
        RuleFor(transaction => transaction.UserId).MustAsync(async (userId,_) => 
            await _repository.Exists(userId)).WithMessage(ResourceMessagesException.USER_NOT_FOUND);

        RuleFor(transaction => transaction).MustAsync(async (transaction, _) =>
    {
        //Verificar se o usuário possui mais de 18 anos. Caso tenha sido enviado uma receita e o usuário seja menor de 18, será retornado uma mensagem de erro.
        return !(await _repository.IsUnderage(transaction.UserId) && transaction.Types == Domain.Enums.TransactionTypes.revenue);
    })
            .WithMessage(ResourceMessagesException.INVALID_TRANSACTION_FOR_UNDERAGE);
    }

}
