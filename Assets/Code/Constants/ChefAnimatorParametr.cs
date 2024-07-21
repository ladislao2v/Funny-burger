using UnityEngine;

namespace Code.Constants
{
    public static class ChefAnimatorParametr
    {
        public static readonly int TaskStarted = Animator.StringToHash(nameof(TaskStarted));
        public static readonly int TaskEnded = Animator.StringToHash(nameof(TaskEnded));
        public static readonly int Moving = Animator.StringToHash(nameof(Moving));
    }
}