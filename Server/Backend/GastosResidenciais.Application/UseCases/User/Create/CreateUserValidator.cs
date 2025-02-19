using FluentValidation;
using GastosResidenciais.Communication.Requests.UserRequests;
using GastosResidenciais.Exceptions;


namespace GastosResidenciais.Application.UseCases.User.Create;

//Classe responsável por validar as informações enviadas na request. No caso, irá verificar se o nome ou a idade foram de fato enviadas.
public class CreateUserValidator : AbstractValidator<RequestCreateUserJson>
{
    public CreateUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        //Não permite caracteres especiais, impedindo SQL Injection
        RuleFor(user => user.Name).Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage(ResourceMessagesException.NAME_SPECIAL_CARACTERS);
        RuleFor(user => user.Name).MaximumLength(25).WithMessage(ResourceMessagesException.NAME_MAX_LENGTH);
        RuleFor(user => user.Age).NotEmpty().WithMessage(ResourceMessagesException.AGE_EMPTY);
        RuleFor(user => user.Age).LessThan(100).WithMessage(ResourceMessagesException.AGE_LESSER_THAN);
    }
}
