using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Pool
{
    public interface IPool
    {
        public UniTask Initialize(string assetKey, Transform container = null, int count = 25);
        public UniTask<T> Get<T>(Vector3 position);
    }
}