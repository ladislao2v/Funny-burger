using Code.Units.Commands;
using System;
using Code.Movement;
using UnityEngine;

namespace Code.Units
{
    public sealed class Client : MonoBehaviour, IClient
    {
        public IMovement Movement { get; private set; }
        public Transform Transform { get; private set; }

        private void Awake()
        {
            Movement = GetComponent<IMovement>();
            Transform = GetComponent<Transform>();
        }

        public void Do(ICommand command, Action onDo = null)
        {
            command.Execute();
            onDo?.Invoke();
        }
    }
}
