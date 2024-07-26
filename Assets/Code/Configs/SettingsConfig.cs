using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create GameConfig", fileName = "GameConfig", order = 0)]
    public class SettingsConfig : ScriptableObject, IChefConfig
    {
        [field: Header("Chef")]
        [field: SerializeField, Range(0, 10)] public float Speed { get; private set; }
        [field: SerializeField, Range(0, 3)] public float TaskTime { get; private set; }
    }

    public interface IChefConfig
    {
        public float Speed { get; }
        public float TaskTime { get; }
    }
}