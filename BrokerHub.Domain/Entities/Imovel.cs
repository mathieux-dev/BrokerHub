using BrokerHub.Domain.Enums;
using BrokerHub.Domain.ValueObjects;

namespace BrokerHub.Domain.Entities;

public class Imovel(string titulo, TipoImovel tipoImovel, Endereco endereco, double area, decimal preco, StatusImovel status)
{
    public int Id { get; private set; }
    public string Titulo { get; private set; } = titulo;
    public TipoImovel Tipo { get; private set; } = tipoImovel;
    public Endereco Endereco { get; private set; } = endereco;
    public double Area { get; private set; } = area;
    public int? Quartos { get; private set; }
    public int? Banheiros { get; private set; }
    public int? VagasGaragem { get; private set; }
    public decimal Preco { get; private set; } = preco;
    public DateTime DataCriacao { get; private set; } = DateTime.Now;
    public StatusImovel Status { get; private set; } = status;

    public void Atualizar(string titulo, TipoImovel tipoImovel, Endereco endereco, double area, decimal preco, StatusImovel status)
    {
        Titulo = titulo;
        Tipo = tipoImovel;
        Endereco = endereco;
        Area = area;
        Preco = preco;
        Status = status;
    }
}
