using BrokerHub.Domain.Enums;
using BrokerHub.Domain.ValueObjects;

namespace BrokerHub.Application.Queries.Imovel.GetImovel;

public class GetImovelResponse(int id, string titulo, TipoImovel tipo, Endereco endereco, double area, int quartos, int banheiros, int vagasGaragem, decimal preco, DateTime dataCriacao, StatusImovel status)
{
    public int Id { get; private set; } = id;
    public string Titulo { get; private set; } = titulo;
    public TipoImovel Tipo { get; private set; } = tipo;
    public Endereco Endereco { get; private set; } = endereco;
    public double Area { get; private set; } = area;
    public int Quartos { get; private set; } = quartos;
    public int Banheiros { get; private set; } = banheiros;
    public int VagasGaragem { get; private set; } = vagasGaragem;
    public decimal Preco { get; private set; } = preco;
    public DateTime DataCriacao { get; private set; } = dataCriacao;
    public StatusImovel Status { get; private set; } = status;
}
