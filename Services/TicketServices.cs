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
        if (ticket == null)
            throw new ArgumentNullException(nameof(ticket));

        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Tickets.Add(ticket);
        return await contexto.SaveChangesAsync() > 0;
    }


    public async Task<bool> Modificar(Tickets tickets)
    {
        if (tickets == null)
            throw new ArgumentNullException(nameof(tickets));

        await using var contexto = await _dbFactory.CreateDbContextAsync();

        if (!await contexto.Tickets.AnyAsync(t => t.TicketsId == tickets.TicketsId))
            return false; 

        contexto.Tickets.Update(tickets);
        return await contexto.SaveChangesAsync() > 0;
    }


    public async Task<bool> Guardar(Tickets tickets)
    {
        return await (await Existe(tickets.TicketsId) ? Modificar(tickets) : Insertar(tickets));
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
            .FirstOrDefaultAsync(t => t.TicketsId == id) ?? null;
    }

    public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> criterio)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AsNoTracking()
            .Where(criterio)
        .ToListAsync();
    }

    public async Task<List<Tickets>> ObtenerLista(int page = 1, int pageSize = 10)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

}


