using BrokerHub.Application.Command.Imovel;
using BrokerHub.Application.Command.Imovel.CreateImovel;

namespace BrokerHub.Application.Common;

public class ApiResponse<T>
{
    public bool Success { get; private set; }
    public string Message { get; private set; }
    public T? Data { get; private set; }

    private ApiResponse(bool success, string message, T? data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static ApiResponse<T> SuccessResult(T data, string message = "Operação completa com sucesso.")
    {
        return new ApiResponse<T>(true, message, data);
    }

    public static ApiResponse<T> FailureResult(string message)
    {
        return new ApiResponse<T>(false, message, default);
    }
}
