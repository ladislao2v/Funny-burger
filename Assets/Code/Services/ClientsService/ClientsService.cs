using System.Collections.Generic;
using System.Threading;
using Code.Services.ClientLoyaltyService;
using Code.Services.Factories.ClientFactory;
using Code.Units;
using Code.Units.Commands;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Services.ClientsService
{
    public sealed class ClientsService : MonoBehaviour, IClientsService
    {
        private readonly Queue<IClient> _clients = new();

        [SerializeField] private int _clientsCount;
        
        [Header("Clients spawn")]
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _spawn;
        
        [Header("Order window")]
        [SerializeField] private Transform _window;
        [SerializeField] private float _windowDuration;
        [SerializeField, Min(0)] private float _windowClearDistance = 3f;
        
        [Header("Away")]
        [SerializeField] private Transform _away;
        [SerializeField] private float _awayDuration;

        [Header("Queue")] 
        [SerializeField] private float _queueMoveDuration;

        private IClientFactory _clientFactory;
        private IClientLoyaltyService _clientLoyaltyService;
        private IClient _current;
        private IClient _leaving;
        private int _spawnedClients;

        public bool IsSend => _current != null;
        public bool HasQueue => _clients.Count > 0;
        public bool IsWindowFree => _current == null && !IsLeavingNearWindow();
        
        private float ClientInterval => _awayDuration + _windowDuration;

        [Inject]
        private void Construct(IClientFactory clientFactory, IClientLoyaltyService clientLoyaltyService)
        {
            _clientFactory = clientFactory;
            _clientLoyaltyService = clientLoyaltyService;
        }

        public async UniTask LoadClients()
        {
            while (_spawnedClients < _clientsCount)
                await SpawnClient();
        }

        public async UniTask LoadTutorialClient()
        {
            if (_spawnedClients == 0)
                await SpawnClient();
        }

        private async UniTask SpawnClient()
        {
            _spawnedClients++;

            Vector3 position = GetPosition(_clients.Count);

            _clients.Enqueue(await _clientFactory.Create(position, transform));
        }

        public void SendClientToWindow()
        {
            IClient client = DequeueClient();

            float duration = Mathf.Max(_windowDuration, 
                GetQueueMoveDuration(client.Transform.position, _window.position));
            
            ICommand moveToWindow = 
                new MoveTo(client, _window, duration);

            client.Do(moveToWindow, () => SetCurrentOnArrival(client).Forget());
        }

        public void SendClientAway()
        {
            if (_current == null)
                return;

            IClient client = _current;
            _current = null;
            _leaving = client;

            ICommand goAway = 
                new MoveTo(client, _away, _awayDuration);
            
            client.Do(goAway, () => BackClientToQueue(client).Forget());
        }

        private async UniTaskVoid SetCurrentOnArrival(IClient client)
        {
            await UniTask.WaitUntil(() => client.Movement.IsMoving.Value == false, 
                cancellationToken: destroyCancellationToken);

            _current = client;
        }

        private IClient DequeueClient()
        {
            IClient last = _clients.Dequeue();
            IClient previos = last;

            foreach (var client in _clients)
            {
                ICommand moveToPrevios = new MoveTo(client, previos.Transform,
                    GetQueueMoveDuration(client.Transform.position, previos.Transform.position));
                
                client.Do(moveToPrevios);

                previos = client;
            }

            return last;
        }

        private async UniTaskVoid BackClientToQueue(IClient client)
        {
            CancellationToken token = destroyCancellationToken;
            
            await UniTask.WaitForSeconds(_awayDuration, cancellationToken: token);
            await UniTask.WaitUntil(() => client.Movement.IsMoving.Value == false, cancellationToken: token);

            if (_leaving == client)
                _leaving = null;
            
            client.Hide();
            
            await UniTask.WaitForSeconds(GetExtraClientInterval(), cancellationToken: token);
            
            Vector3 spawnPosition = GetPosition(_clientsCount);
            Vector3 queuePosition = GetPosition(_clients.Count);
            
            client.Show(spawnPosition);
            _clients.Enqueue(client);

            ICommand moveToQueue = new MoveTo(client, queuePosition, 
                GetQueueMoveDuration(spawnPosition, queuePosition));
            
            client.Do(moveToQueue);
        }

        private bool IsLeavingNearWindow() =>
            _leaving != null &&
            Vector3.Distance(_leaving.Transform.position, _window.position) < _windowClearDistance;

        private float GetQueueMoveDuration(Vector3 from, Vector3 to)
        {
            float places = Vector3.Distance(from, to) / _offset.magnitude;
            
            return _queueMoveDuration * Mathf.Max(1f, places);
        }

        private float GetExtraClientInterval() => 
            _clientLoyaltyService.GetClientInterval(ClientInterval) - ClientInterval;

        private Vector3 GetPosition(int queuePosition) => 
            _spawn.position + queuePosition * _offset;
    }
}