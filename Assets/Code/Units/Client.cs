using Code.Units.Commands;
using System;
using Code.Movement;
using UnityEngine;

namespace Code.Units
{
    public sealed class Client : MonoBehaviour, IClient
    {
        private ClientAgentMovement _agentMovement;
        
        public IMovement Movement { get; private set; }
        public Transform Transform { get; private set; }

        private void Awake()
        {
            _agentMovement = GetComponent<ClientAgentMovement>();
            Movement = _agentMovement;
            Transform = GetComponent<Transform>();
        }

        public void Teleport(Vector3 position) => 
            _agentMovement.Teleport(position);

        public void Show(Vector3 position)
        {
            Transform.position = position;
            gameObject.SetActive(true);
        }

        public void Hide() => 
            gameObject.SetActive(false);

        public void Do(ICommand command, Action onDo = null)
        {
            command.Execute();
            onDo?.Invoke();
        }
    }
}
