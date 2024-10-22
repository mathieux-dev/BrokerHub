using BrokerHub.Domain.Enums;
using BrokerHub.Domain.ValueObjects;

namespace BrokerHub.Domain.Entities;

public class Imovel
{
    private Imovel() { }

    public Imovel(string titulo, TipoImovel tipo, Endereco endereco, double area, int quartos, int banheiros, int vagasGaragem, decimal preco, StatusImovel status)
    {
        Titulo = titulo;
        Tipo = tipo;
        Endereco = endereco;
        Area = area;
        Quartos = quartos;
        Banheiros = banheiros;
        VagasGaragem = vagasGaragem;
        Preco = preco;
        Status = status;
        DataCriacao = DateTime.Now;
    }

    public int Id { get; private set; }
    public string Titulo { get; private set; }
    public TipoImovel Tipo { get; private set; }
    public Endereco Endereco { get; private set; }
    public double Area { get; private set; }
    public int Quartos { get; private set; }
    public int Banheiros { get; private set; }
    public int VagasGaragem { get; private set; }
    public decimal Preco { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public StatusImovel Status { get; private set; }

    public void Update(string titulo, TipoImovel tipo, Endereco endereco, double area, int quartos, int banheiros, int vagasGaragem, decimal preco, StatusImovel status)
    {
        Titulo = titulo;
        Tipo = tipo;
        Endereco = endereco;
        Area = area;
        Quartos = quartos;
        Banheiros = banheiros;
        VagasGaragem = vagasGaragem;
        Preco = preco;
        Status = status;
    }
}
