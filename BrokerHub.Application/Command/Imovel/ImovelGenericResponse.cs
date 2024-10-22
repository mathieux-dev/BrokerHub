namespace BrokerHub.Application.Command.Imovel;

public class ImovelGenericResponse(int id, string titulo, string tipo)
{
    public int ImovelId { get; private set; } = id;
    public string Titulo { get; private set; } = titulo;
    public string Tipo { get; private set; } = tipo;
}
