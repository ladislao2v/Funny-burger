using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create Reward", fileName = "Reward", order = 0)]
    public class RewardConfig : ScriptableObject
    {
        [field: SerializeField] public int RequiredLevel { get; private set; }
        [field: SerializeField] public Item Item { get; private set; }
    }
}