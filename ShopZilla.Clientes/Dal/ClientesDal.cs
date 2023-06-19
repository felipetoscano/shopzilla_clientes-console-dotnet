using ShopZilla.Clientes.Entities;

namespace ShopZilla.Clientes.Dal
{
    public class ClientesDal
    {
        private readonly ClientesDbContext _dbContext;

        public ClientesDal(ClientesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ClienteEntity BuscarClientePorId(int id) => _dbContext.Clientes.FirstOrDefault(cliente => cliente.Id == id);
    }
}
