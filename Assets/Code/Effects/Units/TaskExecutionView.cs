using Code.Constants;
using Code.Units;
using UnityEngine;
using Zenject;

namespace Code.Effects.Units
{
    [RequireComponent(typeof(Animator))]
    public class TaskExecutionView : MonoBehaviour
    {
        private Animator _animator;
        private IPlayer _player;

        [Inject]
        private void Construct(IPlayer player)
        {
            _player = player;
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _player.TaskStarted += OnTaskStarted;
            _player.TaskEnded += OnTaskEnded;
        }

        private void OnDisable()
        {
            _player.TaskStarted -= OnTaskStarted;
            _player.TaskEnded -= OnTaskEnded;
        }

        private void OnTaskEnded() => _animator
            .SetTrigger(ChefAnimatorParametr.TaskEnded);

        private void OnTaskStarted() => _animator.
            SetTrigger(ChefAnimatorParametr.TaskStarted);
    }
}