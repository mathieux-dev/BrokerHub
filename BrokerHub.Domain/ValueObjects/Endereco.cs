namespace BrokerHub.Domain.ValueObjects;
public class Endereco(string rua, string numero, string cidade, string estado, string cep, string? complemento = null)
{
    public string Rua { get; private set; } = rua;
    public string Numero { get; private set; } = numero;
    public string Cidade { get; private set; } = cidade;
    public string Estado { get; private set; } = estado;
    public string CEP { get; private set; } = cep;
    public string? Complemento { get; private set; } = complemento;
}
