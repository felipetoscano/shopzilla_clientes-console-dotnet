using Microsoft.EntityFrameworkCore;
using ShopZilla.Clientes;
using ShopZilla.Clientes.BackgroundServices;
using ShopZilla.Clientes.Dal;
using ShopZilla.Clientes.Models;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        services.AddDbContext<ClientesDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ClientesDb")));
        services.AddSingleton(configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>());
        services.AddSingleton(configuration.GetSection(nameof(KafkaSettings)).Get<KafkaSettings>());
        services.AddScoped<ClientesDal>();
        services.AddHostedService<KafkaConsumerService>();
    })
.Build().Run();