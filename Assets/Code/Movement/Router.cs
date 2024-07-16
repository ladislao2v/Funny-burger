using UnityEngine;

namespace Code.Movement
{
    public abstract class Router
    {
        protected abstract void Rout(IMovement movement, Vector3 direction, float speed);
    }
}