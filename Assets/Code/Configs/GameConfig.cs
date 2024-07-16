using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create GameConfig", fileName = "GameConfig", order = 0)]
    public class GameConfig : ScriptableObject, IChefConfig
    {
        [field: Header("Chef")]
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float TaskTime { get; private set; }
    }

    public interface IChefConfig
    {
        public float Speed { get; }
        public float TaskTime { get; }
    }
}