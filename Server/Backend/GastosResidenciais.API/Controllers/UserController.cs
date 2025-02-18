using GastosResidenciais.Application.UseCases.User.Create;
using GastosResidenciais.Application.UseCases.User.Delete;
using GastosResidenciais.Application.UseCases.User.Get;
using GastosResidenciais.Application.UseCases.User.GetAll;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    //Faz retornar um status code 201 e um objeto Json informando o nome da pessoa cadastrada.
    [ProducesResponseType(typeof(ResponseCreatedUserJson), StatusCodes.Status201Created)]

    //O use Case está sendo passado por injeção de dependencia através da interface ICreateUserUseCase
    public async Task<IActionResult> Create(
        [FromServices]ICreateUserUseCase useCase,
        [FromBody]RequestCreateUserJson request)
    {

        var result = await useCase.Execute(request); 

        return Created(string.Empty, result);
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(typeof(ResponseDeletedUserJson), StatusCodes.Status204NoContent)]

    public async Task <IActionResult> Delete(
        [FromServices]IDeleteUserUseCase useCase,
        [FromRoute]long userId)
    {
        var result = await useCase.Execute(new RequestDeleteUserJson {Id = userId });

        return Ok(result);
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(ResponseGetUserJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser(
        [FromServices] IGetUserUseCase useCase,
        [FromRoute] long userId)
    {
        var result = await useCase.Execute(new RequestGetUserJson { Id = userId });

        return Ok(result);
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(ResponseGetAllUsersJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers(
    [FromServices] IGetAllUsersUseCase useCase)
    {
        var result = await useCase.Execute();
        return Ok(result);
    }


}
