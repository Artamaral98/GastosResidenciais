using Microsoft.Extensions.Configuration;

namespace GastosResidenciais.Infrastructure.Extensions
{
    //Evitar duplicação de código no que tange a connectionString, a partir de uma extensão para o IConfiguration, não necessitando mais do método GetConnectionString.
    public static class ConfigurationExtension
    {
        public static string ConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("Connection")!;

        }
    }
}

