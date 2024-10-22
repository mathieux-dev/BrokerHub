namespace BrokerHub.Domain.ValueObjects;

public class Endereco
{
    private Endereco() { }

    public Endereco(string rua, string numero, string cidade, string estado, string cep, string? complemento = "N/A")
    {
        Rua = rua;
        Numero = numero;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
        Complemento = complemento;
    }

    public string Rua { get; private set; }
    public string Numero { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }
    public string CEP { get; private set; }
    public string? Complemento { get; private set; }
}