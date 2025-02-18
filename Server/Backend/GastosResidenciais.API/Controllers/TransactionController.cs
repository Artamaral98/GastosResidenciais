using GastosResidenciais.Application.UseCases.Transactions.Create;
using GastosResidenciais.Application.UseCases.Transactions.Delete;
using GastosResidenciais.Application.UseCases.Transactions.Get;
using GastosResidenciais.Application.UseCases.Transactions.GetAll;
using GastosResidenciais.Application.UseCases.User.Delete;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.API.Controllers;

[Route("[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(List<ResponseGetTransactionJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransactions(
        [FromServices] IGetTransactionUseCase useCase,
        [FromRoute] long userId)
    {
        var transactions = await useCase.Execute(new RequestGetTransactionJson {UserId = userId });
        return Ok(transactions);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreateTransactionJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTransaction(
    [FromServices] ICreateTransactionUseCase useCase,
    [FromBody] RequestCreateTransactionJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(List<ResponseGetAllTransactionsJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTransactions(
        [FromServices] IGetAllTransactionsUseCase useCase)
    {
        var result = await useCase.Execute();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResponseDeletedUserJson), StatusCodes.Status204NoContent)]

    public async Task<IActionResult> Delete(
    [FromServices] IDeleteTransactionUseCase useCase,
    [FromRoute] long id)
    {
        var result = await useCase.Execute(new RequestDeleteTransactionJson { Id = id });

        return Ok(result);
    }

}