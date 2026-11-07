using Cysharp.Threading.Tasks;

namespace Code.Services.ClientsService
{
    public interface IClientsService
    {
        bool IsSend { get; }
        bool HasQueue { get; }
        bool IsWindowFree { get; }

        UniTask LoadClients();
        UniTask LoadTutorialClient();
        void SendClientToWindow();
        void SendClientAway();
    }
}