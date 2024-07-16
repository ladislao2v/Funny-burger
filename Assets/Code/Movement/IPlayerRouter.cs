using UnityEngine;

namespace Code.Movement
{
    public interface IPlayerRouter
    {
        void Rout(IMovement movement, Vector3 direction);
    }
}