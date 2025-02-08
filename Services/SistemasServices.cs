using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class SistemasServices
{
    private readonly IDbContextFactory<Contexto> _dbFactory;

    public SistemasServices(IDbContextFactory<Contexto> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AnyAsync(s => s.SistemasId == id);
    }

    public async Task<bool> Insertar(Sistemas sistemas)
    {
        if (sistemas == null)
            throw new ArgumentNullException(nameof(sistemas));

        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Sistemas.Add(sistemas);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Sistemas sistemas)
    {
        if (sistemas == null)
            throw new ArgumentNullException(nameof(sistemas));

        await using var contexto = await _dbFactory.CreateDbContextAsync();

        if (!await contexto.Sistemas.AnyAsync(s => s.SistemasId == sistemas.SistemasId))
            return false;

        contexto.Sistemas.Update(sistemas);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Sistemas sistema)
    {
        return await (await Existe(sistema.SistemasId) ? Modificar(sistema) : Insertar(sistema));
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var eliminados = await contexto.Sistemas
            .Where(s => s.SistemasId == id)
            .ExecuteDeleteAsync();
        return eliminados > 0;
    }

    public async Task<Sistemas?> Buscar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AsNoTracking()
            .FirstOrDefaultAsync(s => s.SistemasId == id) ?? null;
    }

    public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<List<Sistemas>> ObtenerLista(int page = 1, int pageSize = 10)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}

