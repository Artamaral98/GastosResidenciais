using GastosResidenciais.Exceptions.ExceptionsBase;

namespace GastosResidenciais.Exceptions;

//O ErrorOnValidationException deverá conter uma lista de mensagens de erros
public class ErrorOnValidationException : GastosResidenciaisException
{
    public IList<string> ErrorMessages { get; set; }

    //Inserindo a lista de erros no construtor
    public ErrorOnValidationException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
