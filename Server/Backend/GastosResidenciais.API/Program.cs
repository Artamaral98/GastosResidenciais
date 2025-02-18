using GastosResidenciais.API.Filters;
using GastosResidenciais.Infrastructure;
using GastosResidenciais.Application;
using GastosResidenciais.Infrastructure.Extensions;
using GastosResidenciais.Infrastructure.Migrations;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:49918") // Porta do frontend 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter))); //Adicionando um novo filtro de exceções na aplicação
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

//Configuraççao para as headers do Cors
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
    await next();
});

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

app.Run();


//Chamar a função do dataBaseMigrations para acessar o DB ou criar o DB, caso ainda não exista.
void MigrateDatabase()
{
    var connectionString = builder.Configuration.ConnectionString();
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope(); //Recuperando o Service Provider que cria o escopo para utilização da injeção de dependencia

    DataBaseMigrations.Migrate(connectionString, serviceScope.ServiceProvider);
}