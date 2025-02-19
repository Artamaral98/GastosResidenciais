using FluentValidation;
using GastosResidenciais.Communication.Requests.UserRequests;
using GastosResidenciais.Domain.Repositories.User;
using GastosResidenciais.Exceptions;

namespace GastosResidenciais.Application.UseCases.User.Get;

public class GetUserValidator : AbstractValidator<RequestGetUserJson>
{
    private readonly IUserRepository _repository;
    public GetUserValidator(IUserRepository repository)
    {
        _repository = repository;

        RuleFor(user => user.Id).NotEmpty().WithMessage(ResourceMessagesException.ID_EMPTY);

        RuleFor(user => user.Id).MustAsync(async (userId, _) => 
        await _repository.Exists(userId)).WithMessage(ResourceMessagesException.USER_NOT_FOUND);
    }
}
