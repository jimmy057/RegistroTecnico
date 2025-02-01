using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;
using System.Net.Sockets;

namespace RegistroTecnico.Services;

public class TicketServices
{
    private readonly IDbContextFactory<Contexto> _dbFactory;

    public TicketServices(IDbContextFactory<Contexto> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AnyAsync(t => t.TicketsId == id);
    }

    public async Task<bool> Insertar(Tickets ticket)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Tickets.Add(ticket);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Tickets tickets)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Tickets.Update(tickets);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Tickets tickets)
    {
        if (!await Existe(tickets.TicketsId))
            return await Insertar(tickets);
        else
            return await Modificar(tickets);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var eliminados = await contexto.Tickets
            .Where(t => t.TicketsId == id)
            .ExecuteDeleteAsync();
        return eliminados > 0;
    }

    public async Task<Tickets?> Buscar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AsNoTracking()
            .FirstOrDefaultAsync(t => t.TicketsId == id);
    }

    public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> criterio)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AsNoTracking()
            .Where(criterio)
        .ToListAsync();
    }

    public async Task<List<Tickets>> ObtenerLista()
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AsNoTracking().ToListAsync();
    }
}


