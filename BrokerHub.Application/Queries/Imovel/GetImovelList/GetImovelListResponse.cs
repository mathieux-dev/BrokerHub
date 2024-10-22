using BrokerHub.Application.Queries.Imovel.GetImovel;

namespace BrokerHub.Application.Queries.Imovel.GetImovelList;

public class GetImovelListResponse
{
    public IEnumerable<GetImovelResponse> Imoveis { get; set; } = [];
}