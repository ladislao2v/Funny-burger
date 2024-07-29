using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ClientAgentMovement : MonoBehaviour, IMovement
    {
        private readonly ReactiveProperty<bool> _isMoving = new();

        private NavMeshAgent _navMeshAgent;

        public IReactiveProperty<bool> IsMoving => _isMoving;

        private void Awake() => 
            _navMeshAgent = GetComponent<NavMeshAgent>();

        public async void Move(Vector3 position, float duration)
        {
            _isMoving.Value = true;
            _navMeshAgent.Move(position);

            await UniTask.WaitForSeconds(duration);

            _isMoving.Value = false;
        }
    }
}