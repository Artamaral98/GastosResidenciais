using AutoMapper;
using GastosResidenciais.Communication.Requests.TransactionRequests;
using GastosResidenciais.Communication.Requests.UserRequests;
using GastosResidenciais.Communication.Responses.TransactionResponses;
using GastosResidenciais.Communication.Responses.UserResponses;
using GastosResidenciais.Domain.Entities;

namespace GastosResidenciais.Application.Services.AutoMapper;

public class Automapping : Profile
{
    public Automapping()
    {
        RequestToDomain();
        DomainToResponse();
    }

    //Configuração do mapeamento de todas as requests para os domains. O autoMapper faz com que não seja necessário o instanciamento manual de classes. evitando new ResponseGetUserJson {}...
    private void RequestToDomain()
    {
        //Mapemaneto para request de Users, utilizado em CreateUserUseCase
        CreateMap<RequestCreateUserJson, Domain.Entities.User>();
        CreateMap<RequestCreateTransactionJson, Transactions>();
        CreateMap<RequestUpdateTransactionJson, Transactions>();
    }
    //Configuração do mapeamento para as responses.
    private void DomainToResponse()
    {
        //Mapeamento das Responses para User
        CreateMap<User, ResponseGetUserJson>()
            .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions));

        //mapeamento de transactions para TransactionResponse
        CreateMap<Transactions, TransactionResponse>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<User, UserSummaryResponse>();

        

        //Mapemaneto para GetTransactions
        CreateMap<Transactions, ResponseGetTransactionJson>()
            .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types.ToString())); //Converte o Types para string, para retornar Receita ou Despesa em vez de 0 ou 1

        //Mapeamento para resposta de criação de transações
        CreateMap<Transactions, ResponseCreateTransactionJson>()
            .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types.ToString()));

        //Mapeamento para response para todas as transações com as informaçoes de seus respectivos usuários
        CreateMap<Transactions, ResponseGetAllTransactionsJson>()
            .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types.ToString()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.User.Age));

        CreateMap<Transactions, ResponseUpdatedTransactionJson>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
            .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types.ToString()));
    }
}
