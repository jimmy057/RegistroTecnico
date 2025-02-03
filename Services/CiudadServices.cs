using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq;
using System.Linq.Expressions;

namespace RegistroTecnico.Services
{
    public class CiudadServices
    {
        private readonly IDbContextFactory<Contexto> DbFactory;

        public CiudadServices(IDbContextFactory<Contexto> DbFactory)
        {
            this.DbFactory = DbFactory;
        }

        public async Task<bool> Existe(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Ciudad.AnyAsync(c => c.CiudadId == id);
        }
        private async Task<bool> Insertar(Ciudad ciudad)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Ciudad.Add(ciudad);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Ciudad ciudad)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Ciudad.Update(ciudad);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Ciudad ciudad)
        {
            if (!await Existe(ciudad.TecnicoId))
                return await Insertar(ciudad);
            else
                return await Modificar(ciudad);
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var Ciudad = await contexto.Ciudad
                .Where(c => c.CiudadId == id).ExecuteDeleteAsync();
            return Ciudad > 0;
        }

        public async Task<Ciudad?> Buscar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Ciudad
                .FirstOrDefaultAsync(t => t.CiudadId == id);
        }

        public async Task<Ciudad?> BuscarNombres(string nombre)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Ciudad
                .FirstOrDefaultAsync(c => c.Name == nombre);
        }

        public async Task<List<Ciudad>> Listar(Expression<Func<Ciudad, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Ciudad
                .Where(criterio)
                .ToListAsync();
        }


    }
}
