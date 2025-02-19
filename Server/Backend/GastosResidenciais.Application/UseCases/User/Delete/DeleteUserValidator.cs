using GastosResidenciais.Exceptions;
using FluentValidation;
using GastosResidenciais.Communication.Requests.UserRequests;

namespace GastosResidenciais.Application.UseCases.User.Delete;

public class DeleteUserValidator : AbstractValidator<RequestDeleteUserJson>
{
    public DeleteUserValidator()
    {
        RuleFor(user => user.Id).NotEmpty().WithMessage(ResourceMessagesException.ID_EMPTY);
    }
}
