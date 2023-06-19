namespace ShopZilla.Clientes.Models
{
    public class KafkaSettings
    {
        public string GroupId { get; init; }
        public Topics Topics { get; init; }
    }

    public class Topics
    {
        public string ConfirmacaoPedido { get; init; }
    }
}
