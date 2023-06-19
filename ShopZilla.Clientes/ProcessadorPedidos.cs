using ShopZilla.Clientes.Entities;

namespace ShopZilla.Clientes
{
    public class ProcessadorPedidos
    {
        public void Processar(PedidoEntity pedido)
        {
            Console.WriteLine($"O status do seu pedido foi atualizado: {pedido.Status}");
        }
    }
}
