using FluentValidation;
using GastosResidenciais.Application.UseCases.User.Get;
using GastosResidenciais.Communication.Requests.TransactionRequests;
using GastosResidenciais.Domain.Repositories.User;
using GastosResidenciais.Exceptions;

namespace GastosResidenciais.Application.UseCases.Transactions.Get;

public class GetTransactionValidator : AbstractValidator<RequestGetTransactionJson>
{
    private readonly IUserRepository _repository;

    public GetTransactionValidator(IUserRepository repository)
    {
        _repository = repository;

        RuleFor(transaction => transaction.UserId).NotEmpty().WithMessage(ResourceMessagesException.ID_EMPTY);

        RuleFor(transaction => transaction.UserId).MustAsync(async (userId, _) =>
        await _repository.Exists(userId)).WithMessage(ResourceMessagesException.USER_NOT_FOUND);

    }

}
