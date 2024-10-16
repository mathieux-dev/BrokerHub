using BrokerHub.Domain.Enums;
using BrokerHub.Domain.Exceptions;
using BrokerHub.Domain.ValueObjects;

namespace BrokerHub.Domain.Entities;

public class Imovel
{
    public int Id { get; private set; }
    public string Titulo { get; private set; }
    public TipoImovel Tipo { get; private set; }
    public Endereco Endereco { get; private set; }
    public double Area { get; private set; }
    public int? Quartos { get; private set; }
    public int? Banheiros { get; private set; }
    public int? VagasGaragem { get; private set; }
    public decimal Preco { get; private set; }
    public DateTime DataCriacao { get; private set; } = DateTime.Now;
    public StatusImovel Status { get; private set; }

    public Imovel(string titulo, TipoImovel tipoImovel, Endereco endereco, double area, decimal preco, StatusImovel status)
    {
        ValidarTitulo(titulo);
        ValidarTipoImovel(tipoImovel);
        ValidarEndereco(endereco);
        ValidarArea(area);
        ValidarPreco(preco);
        ValidarStatusImovel(status);

        Titulo = titulo;
        Tipo = tipoImovel;
        Endereco = endereco;
        Area = area;
        Preco = preco;
        Status = status;
    }

    public void Atualizar(string titulo, TipoImovel tipoImovel, Endereco endereco, double area, decimal preco, StatusImovel status)
    {
        ValidarTitulo(titulo);
        ValidarTipoImovel(tipoImovel);
        ValidarEndereco(endereco);
        ValidarArea(area);
        ValidarPreco(preco);
        ValidarStatusImovel(status);

        Titulo = titulo;
        Tipo = tipoImovel;
        Endereco = endereco;
        Area = area;
        Preco = preco;
        Status = status;
    }

    private static void ValidarTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
        {
            throw new DomainValidationException("O título do imóvel é obrigatório.");
        }
        if (titulo.Length > 150)
        {
            throw new DomainValidationException("O título do imóvel não pode ter mais de 150 caracteres.");
        }
    }

    private static void ValidarTipoImovel(TipoImovel tipoImovel)
    {
        if (!Enum.IsDefined(typeof(TipoImovel), tipoImovel))
        {
            throw new DomainValidationException("O tipo de imóvel informado é inválido.");
        }
    }

    private static void ValidarEndereco(Endereco endereco)
    {
        if (endereco == null)
        {
            throw new DomainValidationException("O endereço do imóvel é obrigatório.");
        }
    }

    private static void ValidarArea(double area)
    {
        if (area <= 0)
        {
            throw new DomainValidationException("A área do imóvel deve ser maior que 0.");
        }
    }

    private static void ValidarPreco(decimal preco)
    {
        if (preco <= 0)
        {
            throw new DomainValidationException("O preço do imóvel deve ser maior que 0.");
        }
    }

    private static void ValidarStatusImovel(StatusImovel status)
    {
        if (!Enum.IsDefined(typeof(StatusImovel), status))
        {
            throw new DomainValidationException("O status do imóvel informado é inválido.");
        }
    }
}
