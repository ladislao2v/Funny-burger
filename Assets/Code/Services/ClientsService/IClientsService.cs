﻿using Cysharp.Threading.Tasks;

namespace Code.Services.ClientsService
{
    public interface IClientsService
    {
        bool IsSend { get; }

        UniTask LoadClients();
        void SendClientToWindow();
        void SendClientAway();
    }
}