using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using ShopZilla.Clientes.Dal;
using ShopZilla.Clientes.Entities;
using ShopZilla.Clientes.Models;
using System.Text.Json;

namespace ShopZilla.Clientes.Services
{
    public class KafkaConsumerService
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly KafkaSettings _kafkaSettings;
        private readonly IServiceProvider _serviceProvider;

        public KafkaConsumerService(ConnectionStrings connectionStrings, KafkaSettings kafkaSettings, IServiceProvider serviceProvider)
        {
            _connectionStrings = connectionStrings;
            _kafkaSettings = kafkaSettings;
            _serviceProvider = serviceProvider;
        }

        public void ConsumirPedidosConfirmados(CancellationToken cancellationToken)
        {
            var config = ObterConfiguracaoConsumidor();
            var consumidor = ObterConsumidorTopicoConfirmacaoPedido(config);

            try
            {
                Console.WriteLine("Consumo iniciado");
                IniciarConsumoTopico(consumidor, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                consumidor.Close();
            }
        }

        private ConsumerConfig ObterConfiguracaoConsumidor()
        {
            return new ConsumerConfig
            {
                BootstrapServers = _connectionStrings.Kafka,
                GroupId = _kafkaSettings.GroupId
            };
        }

        private IConsumer<Ignore, string> ObterConsumidorTopicoConfirmacaoPedido(ConsumerConfig config)
        {
            var consumidor = new ConsumerBuilder<Ignore, string>(config).Build();
            consumidor.Subscribe(_kafkaSettings.Topics.ConfirmacaoPedido);

            return consumidor;
        }

        private void IniciarConsumoTopico(IConsumer<Ignore, string> consumidor, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var mensagem = consumidor.Consume(cancellationToken);
                var pedido = JsonSerializer.Deserialize<PedidoEntity>(mensagem.Message.Value);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var clientesDal = scope.ServiceProvider.GetRequiredService<ClientesDal>();
                    new ProcessadorPedidos().Processar(pedido);
                }

                Console.WriteLine("Registro da fila consumido com sucesso");
            }
        }
    }
}
