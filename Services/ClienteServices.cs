using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq;
using System.Linq.Expressions;

namespace RegistroTecnico.Services
{
    public class ClienteServices
    {
        private readonly IDbContextFactory<Contexto> DbFactory;

        public ClienteServices(IDbContextFactory<Contexto> DbFactory)
        {
            this.DbFactory = DbFactory;
        }

        public async Task<bool> Existe(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes.AnyAsync(c => c.ClienteId == id);
        }

        public async Task<bool> ExistePorNombre(string nombre)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes.AnyAsync(c => c.Nombres == nombre);
        }

        public async Task<bool> ExistePorRnc(string rnc)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes.AnyAsync(c => c.RNC == rnc);
        }

        private async Task<bool> Insertar(Cliente cliente)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Clientes.Add(cliente);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Cliente cliente)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Clientes.Update(cliente);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Cliente cliente)
        {
            if (!await Existe(cliente.TecnicoId))
                return await Insertar(cliente);
            else
                return await Modificar(cliente);
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var Cliente = await contexto.Clientes
                .Where(c => c.ClienteId == id).ExecuteDeleteAsync();
            return Cliente > 0;
        }

        public async Task<Cliente?> Buscar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(t => t.ClienteId == id);
        }

        public async Task<Cliente?> BuscarNombres(string nombre)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Nombres == nombre);
        }

        public async Task<List<Cliente>> Listar(Expression<Func<Cliente, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes.AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<Cliente?> ObtenerClientePorRnc(string rnc)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(c => c.RNC == rnc);
        }


        public async Task<bool> CrearCliente(Cliente cliente)
        {
            if (await ExistePorRnc(cliente.RNC) || await ExistePorNombre(cliente.Nombres))
            {
                return false;
            }

            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Clientes.Add(cliente);
            return await contexto.SaveChangesAsync() > 0;
        }
    }
}
