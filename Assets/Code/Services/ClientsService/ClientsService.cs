using System;
using System.Collections.Generic;
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
        
        [Header("Away")]
        [SerializeField] private Transform _away;
        [SerializeField] private float _awayDuration;

        [Header("Queue")] 
        [SerializeField] private float _queueMoveDuration;

        private IClientFactory _clientFactory;
        private IClient _current;

        public bool IsSend => _current != null;

        [Inject]
        private void Construct(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async UniTask LoadClients()
        {
            for (int i = 0; i < _clientsCount; i++)
                _clients.Enqueue(await _clientFactory.Create(GetPosition(i), transform));
        }

        public void SendClientToWindow()
        {
            IClient client = DequeueClient();

            ICommand moveToWindow = 
                new MoveTo(client, _window, _windowDuration);
            
            client.Do(moveToWindow, () => 
                OnMoved(_windowDuration, () => _current = client)
                );
        }

        public void SendClientAway()
        {
            if(_current == null)
                throw new InvalidOperationException();

            ICommand goAway = 
                new MoveTo(_current, _away, _awayDuration);
            
            _current.Do(goAway, () => 
                OnMoved(_awayDuration, BackClientToQueue)
                );
        }

        private async void OnMoved(float duration, Action callback)
        {
            await UniTask.WaitForSeconds(duration);
            callback.Invoke();
        }

        private IClient DequeueClient()
        {
            IClient last = _clients.Dequeue();
            IClient previos = last;

            foreach (var client in _clients)
            {
                ICommand moveToPrevios = 
                    new MoveTo(client, previos.Transform, _queueMoveDuration);
                
                client.Do(moveToPrevios);

                previos = client;
            }

            return last;
        }

        private void BackClientToQueue()
        {
            _current.Transform.position = GetPosition(_clients.Count);
            _clients.Enqueue(_current);
            _current = null;
        }

        private Vector3 GetPosition(int queuePosition) => 
            _spawn.position + queuePosition * _offset;
    }
}