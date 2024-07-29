using System.Collections.Generic;
using Code.Constants;
using Code.Services.Pool;
using Code.Units;
using Code.Units.Commands;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.ClientsService
{
    public interface IClientsService
    {
        bool IsSend { get; }

        UniTask Load();
        void SendClient();
        void ReturnClient();
    }

    public sealed class ClientsService : MonoBehaviour, IClientsService
    {
        private readonly Queue<IClient> _clients = new();
        private readonly IPool _pool;

        [SerializeField] private int _clientsCount;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _spawn; 
        [SerializeField] private Transform _window;
        [SerializeField] private Transform _away;

        private IClient _current;
        
        public bool IsSend => _current != null;

        public ClientsService(IPool pool)
        {
            _pool = pool;
        }

        public async UniTask Load()
        {
            await _pool.Initialize(AssetKey.Client, transform);

            for (int i = 0; i < _clientsCount; i++)
                _clients.Enqueue(await _pool.Get<IClient>(GetPosition(i)));
        }

        public void SendClient()
        {
            _current = _clients.Dequeue();

            ICommand moveToWindow = 
                new MoveTo(_current, _window);
            
            _current.Do(moveToWindow);
        }

        public void ReturnClient()
        {
            ICommand returnToQueue = 
                new MoveTo(_current, _away, () => _current.Transform.position = GetPosition(_clients.Count));
            
            _current.Do(returnToQueue);
            _clients.Enqueue(_current);
        }

        private Vector3 GetPosition(int queuePosition) => 
            _spawn.position + queuePosition * _offset;
    }
}