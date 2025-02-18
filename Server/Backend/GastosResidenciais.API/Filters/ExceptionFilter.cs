using System.Net;
using GastosResidenciais.Communication.Responses;
using GastosResidenciais.Exceptions;
using GastosResidenciais.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GastosResidenciais.API.Filters;

//Classe responsável pelo filtro de exceções, verifica qual a exceção retornada e utiliza o filtro correspondente, enviando respostas personalizadas.
public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        //Caso a exceção encontrada seja uma exceção reconhecida por esta aplicação, o método HandleProjectException será usado.
        if (context.Exception is GastosResidenciaisException)
            HandleProjectException(context);
        
        else
        {
            ThrowUnknownException(context);
        }
        
    }

    //Método que tem como objetivo identificar qual o tipo de erro encontrado e enviar o status code e a lista dos erros correspondentes.
    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException)
        {
            var exception = context.Exception as ErrorOnValidationException; //Cast para transformar o context.Exception em ErrorOnValidationException

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            //ResponseErrorJson é um objeto criado para conter a lista de erros, evitando que seja enviado ao usuário mensagens de erros com dados sigilosos da aplicação.
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception!.ErrorMessages));
        }
    }

    //Método que será retornado em casos de erros desconhecidos.
    private void ThrowUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
    }
}
