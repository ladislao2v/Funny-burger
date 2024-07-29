using System;
using System.Collections.Generic;
using Code.Services.Factories.PrefabFactory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Pool
{
    public sealed class QueuePool : IPool
    {
        private readonly IPrefabFactory _factory;
        private readonly Queue<GameObject> _pool = new();
        private readonly List<GameObject> _activeObjects = new();
        
        private string _assetKey;
        private Transform _container;

        public QueuePool(IPrefabFactory factory)
        {
            _factory = factory;
        }

        public async UniTask Initialize(string assetKey, Transform container = null, int count = 25)
        {
            _assetKey = assetKey;
            _container = container;

            for(int i = 0; i < count; i++)
                Return(await CreateNew(Vector3.zero));
        }

        public async UniTask<T> Get<T>(Vector3 position)
        {
            if(_activeObjects.Count  > 0)
                Return(_activeObjects.Find(t => t.activeInHierarchy == false));
            
            GameObject item = _pool.Count > 0 ? _pool.Dequeue() : await CreateNew(position);
            item.transform.position = position;
            item.transform.SetParent(_container);
            item.gameObject.SetActive(true);
            _activeObjects.Add(item);

            if(!item.TryGetComponent(out T component))
                throw new ArgumentException(nameof(component));

            return component;
        }

        private async UniTask<GameObject> CreateNew(Vector3 position)
        {
            GameObject gameObject = await _factory.Create(_assetKey);

            gameObject.transform.position = position;

            return gameObject;
        }

        public void Return(GameObject item)
        {
            if(item == null)
                return;

            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
            _activeObjects.Remove(item);
        }
    }
}
