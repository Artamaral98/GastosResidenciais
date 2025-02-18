using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;
using GastosResidenciais.Domain.Repositories;
using GastosResidenciais.Domain.Repositories.User;
using GastosResidenciais.Exceptions;

namespace GastosResidenciais.Application.UseCases.User.Delete;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ISaveChangesUnit _saveChangesUnit;


    public DeleteUserUseCase(IUserRepository userRepository, ISaveChangesUnit saveChangesUnit)
    {
        _userRepository = userRepository;
        _saveChangesUnit = saveChangesUnit;

    }

    public async Task<ResponseDeletedUserJson> Execute(RequestDeleteUserJson request)
    {
        Validate(request);

        var userId = request.Id;

        await _userRepository.Delete(userId);
        await _saveChangesUnit.Commit();

        return new ResponseDeletedUserJson
        {
            Message = "Usuário deletado"
        };
    }


    private void Validate(RequestDeleteUserJson request)
    {
        var validator = new DeleteUserValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false) 
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList(); 
            throw new ErrorOnValidationException(errorMessages);

        }

    }

}
