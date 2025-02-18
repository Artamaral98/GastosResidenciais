using AutoMapper;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;
using GastosResidenciais.Domain.Repositories;
using GastosResidenciais.Domain.Repositories.User;
using GastosResidenciais.Exceptions;

namespace GastosResidenciais.Application.UseCases.User.Create;

public class CreateUserUseCase : ICreateUserUseCase
{

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ISaveChangesUnit _saveChangesUnit;

    //Passando para o construtor, evitando instanciamento repetitivo.
    public CreateUserUseCase(IUserRepository userRepository, IMapper mapper, ISaveChangesUnit saveChangesUnit)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _saveChangesUnit = saveChangesUnit;
    }
    public async Task<ResponseCreatedUserJson> Execute(RequestCreateUserJson request) 
    {
        Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);

        await _userRepository.Add(user);
        await _saveChangesUnit.Commit();

        var response = new ResponseCreatedUserJson
        {
            Name = request.Name
        };

        return response;
    }


    //Função responsável por fazer validações na request através do Fluent Validator.
    private void Validate(RequestCreateUserJson request)
    {
        var validator = new CreateUserValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false) //Retornará true caso a requisição seja válida ou false caso alguma informação esteja inválida.
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList(); //Buscando os erros contidos nos erros da validação e convertendo para o tipo List
            throw new ErrorOnValidationException(errorMessages);

        }

    }
}
