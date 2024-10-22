using BrokerHub.Domain.Entities;
using BrokerHub.Domain.Enums;
using BrokerHub.Domain.Interfaces;
using BrokerHub.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BrokerHub.Infrastructure.Persistence.Repositories;

public class ImovelRepository(BrokerHubDbContext context) : IImovelRepository
{
    private readonly BrokerHubDbContext _context = context;

    public async Task<IEnumerable<Imovel>> GetAllAsync(int page, int pageSize,
        string? filtroTitulo, TipoImovel? filtroTipo, StatusImovel? filtroStatus,
        int? filtroQuartos, int? filtroBanheiros, int? filtroVagasGaragem,
        double? filtroAreaMinima, double? filtroAreaMaxima,
        decimal? filtroPrecoMinimo, decimal? filtroPrecoMaximo,
        string? filtroCidade, string? filtroEstado)
    {
        var query = _context.Imoveis.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtroTitulo))
            query = query.Where(i => i.Titulo.Contains(filtroTitulo));

        if (filtroTipo.HasValue)
            query = query.Where(i => i.Tipo == filtroTipo.Value);

        if (filtroStatus.HasValue)
            query = query.Where(i => i.Status == filtroStatus.Value);

        if (filtroQuartos.HasValue)
            query = query.Where(i => i.Quartos == filtroQuartos.Value);

        if (filtroBanheiros.HasValue)
            query = query.Where(i => i.Banheiros == filtroBanheiros.Value);

        if (filtroVagasGaragem.HasValue)
            query = query.Where(i => i.VagasGaragem == filtroVagasGaragem.Value);

        if (filtroAreaMinima.HasValue)
            query = query.Where(i => i.Area >= filtroAreaMinima.Value);

        if (filtroAreaMaxima.HasValue)
            query = query.Where(i => i.Area <= filtroAreaMaxima.Value);

        if (filtroPrecoMinimo.HasValue)
            query = query.Where(i => i.Preco >= filtroPrecoMinimo.Value);

        if (filtroPrecoMaximo.HasValue)
            query = query.Where(i => i.Preco <= filtroPrecoMaximo.Value);

        if (!string.IsNullOrWhiteSpace(filtroEstado))
            query = query.Where(i => i.Endereco.Estado == filtroEstado);

        if (!string.IsNullOrWhiteSpace(filtroCidade))
            query = query.Where(i => i.Endereco.Cidade == filtroCidade);

        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }



    public async Task<Imovel?> GetByIdAsync(int id)
    {
        return await _context.Imoveis.FindAsync(id);
    }

    public async Task AddAsync(Imovel imovel)
    {
        await _context.Imoveis.AddAsync(imovel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Imovel imovel)
    {
        _context.Imoveis.Update(imovel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var imovel = await GetByIdAsync(id);
        if (imovel != null)
        {
            _context.Imoveis.Remove(imovel);
            await _context.SaveChangesAsync();
        }
    }
}
