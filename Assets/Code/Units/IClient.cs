using Code.Movement;
using UnityEngine;

namespace Code.Units
{
    public interface IClient : IUnit
    {
        public IMovement Movement { get; }
        Transform Transform { get; }
    }
}