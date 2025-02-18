namespace GastosResidenciais.Communication.Responses;

public class ResponseErrorJson
{
    public IList<string> Errors { get; set; }

    public ResponseErrorJson(IList<string> errors) => Errors = errors;

    //O segundo construtor tem a função de permitir que seja passado uma string como mensagem de erro, que será colocada dentro de uma lista instanciada.
    public ResponseErrorJson(string error)
    {
        Errors = new List<string> { error };     
    }


}
