using UnityEngine;

namespace Code.Configs.Chef
{
    [CreateAssetMenu(menuName = "Create ChefConfig", fileName = "ChefConfig", order = 0)]
    public class ChefConfig : ScriptableObject
    {
        public float Speed;
    }
}