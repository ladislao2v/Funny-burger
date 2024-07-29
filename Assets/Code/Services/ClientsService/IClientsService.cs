using Cysharp.Threading.Tasks;

namespace Code.Services.ClientsService
{
    public interface IClientsService
    {
        bool IsSend { get; }

        UniTask LoadClients();
        void SendClient();
        void ReturnClient();
    }
}