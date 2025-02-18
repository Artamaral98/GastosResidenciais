using GastosResidenciais.Application.Services.AutoMapper;
using GastosResidenciais.Application.UseCases.Transactions.Create;
using GastosResidenciais.Application.UseCases.Transactions.Delete;
using GastosResidenciais.Application.UseCases.Transactions.Get;
using GastosResidenciais.Application.UseCases.Transactions.GetAll;
using GastosResidenciais.Application.UseCases.User.Create;
using GastosResidenciais.Application.UseCases.User.Delete;
using GastosResidenciais.Application.UseCases.User.Get;
using GastosResidenciais.Application.UseCases.User.GetAll;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace GastosResidenciais.Application;

//Classe responsável pela injeção de dependência para todo o projeto de application.
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration) //o This está relatando que a função é uma extensão de IServiceCollection
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new Automapping());
        }).CreateMapper());
    }

    private static void AddUseCases(IServiceCollection services)  
    {
        //Sempre que a interface for chamada, haverá um novo instanciamento da função relacionada
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
        services.AddScoped<IGetUserUseCase, GetUserUseCase>();
        services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
        services.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>();
        services.AddScoped<IGetTransactionUseCase, GetTransactionsUseCase>();
        services.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>();
        services.AddScoped<CreateTransactionValidator>();
        services.AddScoped<IGetAllTransactionsUseCase, GetAllTransactionsUseCase>();
        services.AddScoped<IDeleteTransactionUseCase, DeleteTransactionUseCase>();

    }

}
