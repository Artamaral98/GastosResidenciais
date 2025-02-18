namespace Validators.Test.User.Create;

using CommonTestUtilities.Requests;
using FluentAssertions;
using GastosResidenciais.Application.UseCases.User.Create;
using GastosResidenciais.Exceptions;
using Xunit;


//Esta classe tem o objivo de verificar o funcionamento da função de validação contida no useCase de registrar usuário


//Testando o caso de sucesso, todas as informações corretas
public class CreateUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new CreateUserValidator();
        var request = RequestCreateUserJsonBuilder.Build();

        var result = validator.Validate(request);

        //Assert - verificar se o resultado retornado é o esperado
        result.IsValid.Should().BeTrue(); //Utilização  do fluentAssertion para realizar o Assert
    }

    //testando caso em que o input nome será enviado vazio
    [Fact]
    public void Error_Name_Empty()
    {
        var validator = new CreateUserValidator();

        var request = RequestCreateUserJsonBuilder.Build();
        request.Name = string.Empty;

        var result = validator.Validate(request);

        //Verificando se houve erro na validação e se a mensagem de nome vazio foi retornada
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.NAME_EMPTY));
    }


    //testando o limite de caracteres para nome
    [Fact]
    public void Error_Name_Length()
    {
        var validator = new CreateUserValidator();

        var request = RequestCreateUserJsonBuilder.Build();
        request.Name += "aaaaaaaaaaaaaaaaaaaaaaaaaa";

        var result = validator.Validate(request);

        //Verificando se houve erro na validação e se a mensagem de nome vazio foi retornada
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.NAME_MAX_LENGTH));
    }

    [Fact]
    public void Error_Age_Less_Than_100()
    {
        var validator = new CreateUserValidator();

        var request = RequestCreateUserJsonBuilder.Build();
        request.Age = 101;

        var result = validator.Validate(request);

        //Verificando se houve erro na validação e se a mensagem de nome vazio foi retornada
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.AGE_LESSER_THAN));
    }

}
