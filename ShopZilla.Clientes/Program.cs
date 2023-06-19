using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopZilla.Clientes.Dal;
using ShopZilla.Clientes.Models;
using ShopZilla.Clientes.Services;

namespace ShopZilla.Clientes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            var configuration = configurationBuilder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var kafkaConsumer = serviceProvider.GetRequiredService<KafkaConsumerService>();
            var cts = new CancellationTokenSource();
            kafkaConsumer.ConsumirPedidosConfirmados(cts.Token);
        }

        private static void ConfigureServices(IServiceCollection services, IConfigurationRoot config)
        {
            services.AddDbContext<ClientesDbContext>(options => options.UseSqlServer(config.GetConnectionString("ClientesDb")));
            services.AddScoped<ClientesDal>();
            services.AddSingleton(config.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>());
            services.AddSingleton(config.GetSection(nameof(KafkaSettings)).Get<KafkaSettings>());
            services.AddSingleton<KafkaConsumerService>();
        }
    }
}
