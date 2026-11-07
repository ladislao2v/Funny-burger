using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ClientAgentMovement : MonoBehaviour, IMovement
    {
        private const float MaxDurationFactor = 2f;
        private const float ArrivalThreshold = 0.05f;
        
        private readonly ReactiveProperty<bool> _isMoving = new();

        private NavMeshAgent _navMeshAgent;
        private int _moveId;

        public IReactiveProperty<bool> IsMoving => _isMoving;

        private void Awake() => 
            _navMeshAgent = GetComponent<NavMeshAgent>();

        public void Teleport(Vector3 position)
        {
            _moveId++;
            _navMeshAgent.ResetPath();
            _navMeshAgent.Warp(position);
            _isMoving.Value = false;
        }

        public async void Move(Vector3 position, float duration)
        {
            int moveId = ++_moveId;
            
            _isMoving.Value = true;

            _navMeshAgent.SetDestination(position);

            float timeout = duration * MaxDurationFactor;
            float elapsed = 0f;

            do
            {
                bool isCanceled = await UniTask
                    .Yield(PlayerLoopTiming.Update, destroyCancellationToken)
                    .SuppressCancellationThrow();

                if (isCanceled || moveId != _moveId)
                    return;

                elapsed += Time.deltaTime;
            } 
            while (!HasArrived() && elapsed < timeout);
            
            if (moveId != _moveId)
                return;
            
            _navMeshAgent.ResetPath();

            _isMoving.Value = false;
        }

        private bool HasArrived() =>
            !_navMeshAgent.pathPending &&
            _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance + ArrivalThreshold;
    }
}