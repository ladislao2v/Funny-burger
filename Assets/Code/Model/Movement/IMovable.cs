using UniRx;
using UnityEngine;

namespace Code.Model.Movement
{
    public interface IMovable
    {
        public IReactiveProperty<bool> IsMoving { get; }
        
        public void Move(Vector3 direction);
    }
}