using System;
using Code.Constants;
using Code.Services.Factories.PrefabFactory;
using Code.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Factories.ClientFactory
{
    public sealed class ClientFactory : IClientFactory
    {
        private readonly IPrefabFactory _prefabFactory;

        public ClientFactory(IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
        }
        
        public async UniTask<IClient> Create(Vector3 position, Transform parent)
        {
            GameObject gameObject = await _prefabFactory.Create(AssetKey.Client);
            
            gameObject.transform.SetParent(parent);
            gameObject.transform.localPosition = position;

            if (!gameObject.TryGetComponent(out IClient client))
                throw new ArgumentException(nameof(gameObject));

            return client;
        }
    }
}