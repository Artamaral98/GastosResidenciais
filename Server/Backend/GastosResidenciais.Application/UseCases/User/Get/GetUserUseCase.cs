using AutoMapper;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;
using GastosResidenciais.Domain.Repositories.User;
using GastosResidenciais.Exceptions;

namespace GastosResidenciais.Application.UseCases.User.Get;

public class GetUserUseCase : IGetUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public GetUserUseCase(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ResponseGetUserJson> Execute(RequestGetUserJson request)
    {
        await Validate(request);

        var user = await _userRepository.GetUserById(request.Id);

        var response = _mapper.Map<ResponseGetUserJson>(user);

        return response;
    }
    private async Task Validate(RequestGetUserJson request)
    {
        var validator = new GetUserValidator(_userRepository);
        var result = await validator.ValidateAsync(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);

        }

    }
}
