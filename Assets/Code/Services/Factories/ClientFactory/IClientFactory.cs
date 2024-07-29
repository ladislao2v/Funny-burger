using Code.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Factories.ClientFactory
{
    public interface IClientFactory
    {
        public UniTask<IClient> Create(Vector3 position, Transform parent);
    }
}