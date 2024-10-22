using BrokerHub.Domain.Entities;
using BrokerHub.Domain.Enums;

namespace BrokerHub.Domain.Interfaces;

public interface IImovelRepository
{
    Task<IEnumerable<Imovel>> GetAllAsync(int page, int pageSize, string? filtroTitulo, TipoImovel? filtroTipo, StatusImovel? filtroStatus,
                                      int? filtroQuartos, int? filtroBanheiros, int? filtroVagasGaragem, double? filtroAreaMinima,
                                      double? filtroAreaMaxima, decimal? filtroPrecoMinimo, decimal? filtroPrecoMaximo,
                                      string? filtroCidade, string? filtroEstado);
    Task<Imovel?> GetByIdAsync(int id);
    Task AddAsync(Imovel imovel);
    Task UpdateAsync(Imovel imovel);
    Task DeleteAsync(int id);
}
