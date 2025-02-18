using Bogus;
using GastosResidenciais.Communication.Requests;

namespace CommonTestUtilities.Requests;

//Esta classe será responsável por montar requests aleatórias para os testes de criação de usuário
public class RequestCreateUserJsonBuilder
{
    public static RequestCreateUserJson Build()
    {
        //A bilbioteca bugus utiliza a mesma forma de validação do fluentvalidator
        return new Faker<RequestCreateUserJson>()
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Age, (f) => f.Random.Int(1, 100));


    }
}
