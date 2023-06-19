using Microsoft.EntityFrameworkCore;
using ShopZilla.Clientes.Entities;

namespace ShopZilla.Clientes
{
    public class ClientesDbContext : DbContext
    {
        public DbSet<ClienteEntity> Clientes { get; set; }

        public ClientesDbContext(DbContextOptions<ClientesDbContext> options) : base(options) { }
    }
}
