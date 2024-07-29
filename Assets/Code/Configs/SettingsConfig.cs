using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create GameConfig", fileName = "GameConfig", order = 0)]
    public class SettingsConfig : ScriptableObject, IChefConfig, IClientConfig
    {
        [field: Header("Chef")]
        [field: SerializeField, Range(0, 10)] public float Speed { get; private set; }
        [field: SerializeField, Range(0, 3)] public float TaskTime { get; private set; }
        
        [field: Header("Clients")]
        [field: SerializeField, Range(0, 10)] public int Clients { get; private set; }
        [field: SerializeField] public Vector3 ClientSpawnPoint { get; private set; }
        [field: SerializeField] public Vector3 Offset { get; private set; }
    }

    public interface IClientConfig
    {
        int Clients { get; }
        Vector3 ClientSpawnPoint { get; }
        Vector3 Offset { get; }
    }

    public interface IChefConfig
    {
        public float Speed { get; }
        public float TaskTime { get; }
    }
}