using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Factories.PrefabFactory
{
    public interface IPrefabFactory<T> where T : MonoBehaviour
    {
        UniTask<T> Create();
    }
}