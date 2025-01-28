using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Models;


namespace RegistroTecnico.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
        public DbSet<Tecnicos> Tecnicos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Ciudad> Ciudad { get; set; }
    }
}
