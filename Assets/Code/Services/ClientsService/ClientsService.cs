using System;
using System.Collections.Generic;
using Code.Services.Factories.ClientFactory;
using Code.Units;
using Code.Units.Commands;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.ClientsService
{
    public sealed class ClientsService : MonoBehaviour, IClientsService
    {
        private readonly Queue<IClient> _clients = new();
        private readonly IClientFactory _clientFactory;

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

        private IClient _current;

        public bool IsSend => _current != null;

        public ClientsService(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async UniTask LoadClients()
        {
            for (int i = 0; i < _clientsCount; i++)
                _clients.Enqueue(await _clientFactory.Create(GetPosition(i), transform));
        }

        public void SendClient()
        {
            IClient client = DequeueClient();

            ICommand moveToWindow = 
                new MoveTo(_current, _window, _windowDuration);
            
            client.Do(moveToWindow, () => 
                OnMoved(_windowDuration, () => _current = client)
                );
        }

        public void ReturnClient()
        {
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
                    new MoveTo(_current, previos.Transform, _queueMoveDuration);
                
                client.Do(moveToPrevios);
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